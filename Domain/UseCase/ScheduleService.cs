using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Authentication;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Entities.Interfaces;
using Domain.UseCase.Builders;
using Domain.ViewModel;
using Domain.ViewModel.Jwt;
using Infrastructure.Services;
using Infrastructure.Services.Exceptions;
using Infrastructure.Services.Person;

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
    }
}
