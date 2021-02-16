using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.PdfServices;
using Infrastructure.RepositoryServices;

namespace Domain.UseCase
{
    public class PdfService
    {
        public PdfService(IPdfWriter pdfWriter)
        {
            this.pdfWriter = pdfWriter;
        }

        public IPdfWriter pdfWriter { get; }

        public async Task<string> BuildRentalPDF(Schedule schedule, IEntityRepository entityRepository)
        {
            var vehicle = await entityRepository.FindById<Vehicle>(schedule.VehicleId);
            var category = await entityRepository.FindById<Category>(vehicle.CategoryId);
            var model = await entityRepository.FindById<Model>(vehicle.ModelId);
            var brand = await entityRepository.FindById<Brand>(vehicle.BrandId);

            var body = "<hr>";
            body += "<h3>Reserva</h3>";
            body += "<hr>";
            body += $"Data da reserva: {schedule.Date:dd/mm/yyyy HH:MM}<br>";
            body += $"Quantidade de horas alugadas: {schedule.RentalHours}<br>";
            body += $"Data da coleta prevista: {schedule.ExpectedCollective:dd/mm/yyyy HH:MM}<br>";
            body += $"Data de entrega prevista: {schedule.EstimatedDeliveryTime:dd/mm/yyyy HH:MM}<br>";
            body += $"Valor da hora: R${schedule.HourlyValue}";
            body += "<hr>";
            body += "<h3>Reserva do veículo</h3>";
            body += "<hr>";
            body += $"Nome: {vehicle.Name}<br>";
            body += $"Marca: {brand.Name}<br>";
            body += $"Modelo: {model.Name}<br>";
            body += $"Ano: {vehicle.Year}<br>";
            body += $"Categoria: {category.Name}<br>";
            body += $"Capacidade do tanque: {vehicle.TankCapacity}<br>";
            body += $"Capacidade do Porta Malas: {vehicle.LuggageCapacity}";
            body += "<hr>";
            body += $"<h2>Valor Estimado: R${schedule.Subtotal}</h2>";
            body += "<hr>";

            return pdfWriter.Build(body);
        }

        public async Task<string> BuildPaymentPDF(Schedule schedule, IEntityRepository entityRepository)
        {
            var vehicle = await entityRepository.FindById<Vehicle>(schedule.VehicleId);
            var category = await entityRepository.FindById<Category>(vehicle.CategoryId);
            var model = await entityRepository.FindById<Model>(vehicle.ModelId);
            var brand = await entityRepository.FindById<Brand>(vehicle.BrandId);
            var checklist = schedule.Checklist;

            var body = "<hr>";
            body += "<h3>Reserva</h3>";
            body += "<hr>";
            body += $"Data da reserva: {schedule.Date:dd/mm/yyyy HH:MM}<br>";
            body += $"Quantidade de horas alugadas: {schedule.RentalHours}<br>";
            body += $"Data da coleta prevista: {schedule.ExpectedCollective:dd/mm/yyyy HH:MM}<br>";
            body += $"Data de entrega prevista: {schedule.EstimatedDeliveryTime:dd/mm/yyyy HH:MM}<br>";
            body += $"Valor da hora: R${schedule.HourlyValue}";
            body += "<hr>";
            body += "<h3>Dados da entrega</h3>";
            body += "<hr>";
            body += $"Data da coleta: {schedule.CollectiveHeld:dd/mm/yyyy HH:MM}<br>";
            body += $"Data da entrega: {schedule.DeliveryCompleted:dd/mm/yyyy HH:MM}";
            body += "<hr>";
            body += "<h3>Checklist</h3>";
            body += "<hr>";
            body += $"Carro limpo: {(checklist.CleanCar ? "Sim" : "Não")}<br>";
            body += $"Tanque cheio: {(checklist.FullTank ? "Sim" : "Não")}<br>";
            body += $"Tanque litro pendente: {(checklist.PendingCleanCar ? "Sim" : "Não")}<br>";
            body += $"Amassado: {(checklist.Scratches ? "Sim" : "Não")}<br>";
            body += $"Arranhões: {(checklist.Wrinkled ? "Sim" : "Não")}";
            body += "<hr>";
            body += "<h3>Reserva do veículo</h3>";
            body += "<hr>";
            body += $"Nome: {vehicle.Name}<br>";
            body += $"Marca: {brand.Name}<br>";
            body += $"Modelo: {model.Name}<br>";
            body += $"Ano: {vehicle.Year}<br>";
            body += $"Categoria: {category.Name}<br>";
            body += $"Capacidade do tanque: {vehicle.TankCapacity}<br>";
            body += $"Capacidade do Porta Malas: {vehicle.LuggageCapacity}";
            body += "<hr>";
            body += $"<h3>Valor Estimado: R${schedule.Subtotal}</h3>";
            body += $"<h2>Valor Total: R${schedule.Total}</h2>";
            body += "<hr>";

            return pdfWriter.Build(body);
        }
    }
}
