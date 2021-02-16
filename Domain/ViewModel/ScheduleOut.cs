using System;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record ScheduleOut
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("VeiculoId")]
        public int VehicleId { get; set; }
        [JsonPropertyName("HorasLocacao")]
        public double RentalHours { get; set; }
        [JsonPropertyName("Data")]
        public DateTime Date { get; set; }
        [JsonPropertyName("DataHoraColetaPrevista")]
        public DateTime ExpectedCollective { get; set; }
        [JsonPropertyName("DataHoraEntregaPrevista")]
        public DateTime EstimatedDeliveryTime { get; set; }
        [JsonPropertyName("ValorHora")]
        public double HourlyValue { get; set; }
        [JsonPropertyName("SubTotal")]
        public double Subtotal { get; set; }
        [JsonPropertyName("ReciboDePagamento")]
        public string RentalPaymentReceipt { get; set; }
    }
}
