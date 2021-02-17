using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Authentication;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Entities.Exceptions;
using Domain.Entities.Interfaces;
using Domain.UseCase.Builders;
using Domain.ViewModel;
using Domain.ViewModel.Jwt;
using Infrastructure.PdfServices;
using Infrastructure.RepositoryServices;
using Infrastructure.RepositoryServices.Exceptions;
using Infrastructure.RepositoryServices.Person;

namespace Domain.UseCase
{
    public class ScheduleService
    {
        public ScheduleService(IEntityRepository entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        private readonly IEntityRepository entityRepository;

        public async Task<VehicleScheduleSimulationOutput> Simulation(VehicleScheduleSimulationInput scheduleInput)
        {
            var parameters = new List<dynamic>
            {
                new
                {
                    Key = "VehicleId",
                    Value = scheduleInput.VehicleId
                }
            };

            var completeVehicle = await entityRepository.Get<CompleteVehicle>("sp_completeVehicle", parameters);
            if (completeVehicle == null || completeVehicle.Id == 0) throw new EntityNotFound("Veículo não encontrado");

            var vehicleMap = EntityBuilder.Call<VehicleMap>(completeVehicle);
            vehicleMap.Model = new Model() { Id = completeVehicle.ModelId, Name = completeVehicle.Model };
            vehicleMap.Brand = new Brand() { Id = completeVehicle.BrandId, Name = completeVehicle.Brand };
            vehicleMap.Category = new Category() { Id = completeVehicle.CategoryId, Name = completeVehicle.Category };

            return new VehicleScheduleSimulationOutput
            {
                Vehicle = vehicleMap,
                Total = vehicleMap.HourValue * scheduleInput.Hours,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now.AddHours(scheduleInput.Hours)
            };
        }

        public async Task<ScheduleOut> BookCar(ScheduleInput scheduleInput, IPdfWriter pdfWriter, string pathPDF)
        {
            if (scheduleInput.UserId == 0 && scheduleInput.OparatorId == 0) throw new ObligatoryScheduleUserOrOperator("Para fazer a reserva selecione o usuário ou o operador");
            if (scheduleInput.VehicleId == 0) throw new EntityNotFound("Veículo obrigatório");
            var vehicle = await entityRepository.FindById<Vehicle>(scheduleInput.VehicleId);
            if (vehicle == null || vehicle.Id == 0) throw new EntityNotFound("Veículo não identificado");

            var schedule = EntityBuilder.Call<Schedule>(scheduleInput);
            schedule.Date = DateTime.Now;
            schedule.ExpectedCollective = DateTime.Now.AddHours(schedule.HourlyValue + 2); // 2 horas para revisão do veículo
            schedule.CollectiveHeld = schedule.ExpectedCollective;
            schedule.EstimatedDeliveryTime = DateTime.Now.AddHours(schedule.HourlyValue);
            schedule.DeliveryCompleted = schedule.EstimatedDeliveryTime;
            schedule.HourlyValue = vehicle.HourValue;
            schedule.RentalHours = schedule.RentalHours;
            schedule.Subtotal = schedule.RentalHours * vehicle.HourValue;
            schedule.Total = schedule.Subtotal;
            await entityRepository.Save(schedule);
            schedule.Vehicle = vehicle;
            var scheduleOut = EntityBuilder.Call<ScheduleOut>(schedule);
            scheduleOut.RentalPaymentReceipt = await new PdfService(pdfWriter).BuildRentalPDF(schedule, entityRepository, pathPDF);
            return scheduleOut;
        }

        public async Task<ScheduleOut> GetByCPF(string cpf, IPdfWriter pdfWriter, string pathPDF)
        {
            var parameters = new List<dynamic>();
            parameters.Add(new
            {
                Key = "Document",
                Value = cpf
            });

            var schedule = await entityRepository.Get<Schedule>("sp_getScheduleByDocument", parameters);
            var scheduleOut = EntityBuilder.Call<ScheduleOut>(schedule);
            scheduleOut.Vehicle = await entityRepository.FindById<Vehicle>(schedule.VehicleId);
            scheduleOut.RentalPaymentReceipt = await new PdfService(pdfWriter).BuildRentalPDF(schedule, entityRepository, pathPDF);
            return scheduleOut;
        }

        public async Task<SchedulePaymentOut> ReturnPayment(Checklist checklist, IPdfWriter pdfWriter, string pathPDF)
        {
            if (checklist.ScheduleId == 0) throw new EntityNotFound("Identificador do agendamento obrigatorio");
            var schedule = await entityRepository.FindById<Schedule>(checklist.ScheduleId);
            if (schedule == null || schedule.Id == 0) throw new EntityNotFound("Identificador do agendamento não encontrado");

            if (checklist.OperatorId == 0) throw new EntityNotFound("Operador obrigatorio");
            var op = await entityRepository.FindById<Operator>(checklist.OperatorId);
            if (op == null || op.Id == 0) throw new EntityNotFound("Operador não identificado");

            await entityRepository.Save(checklist);

            schedule.CollectiveHeld = DateTime.Now;
            schedule.DeliveryCompleted = DateTime.Now;

            if(!checklist.CleanCar) 
                schedule.Total += (schedule.Subtotal * 30 / 100);
            if (!checklist.FullTank)
                schedule.Total += (schedule.Subtotal * 30 / 100);
            if (!checklist.Wrinkled)
                schedule.Total += (schedule.Subtotal * 30 / 100);
            if (!checklist.Scratches)
                schedule.Total += (schedule.Subtotal * 30 / 100);

            schedule.AdditionalCosts = schedule.Total - schedule.Subtotal;
            schedule.SurveyPerformed = true;
            schedule.Checklist = checklist;

            await entityRepository.Save(schedule);

            return new SchedulePaymentOut()
            {
                Schedule = schedule,
                Invoice = await new PdfService(pdfWriter).BuildPaymentPDF(schedule, entityRepository, pathPDF)
            };
        }
    }
}
