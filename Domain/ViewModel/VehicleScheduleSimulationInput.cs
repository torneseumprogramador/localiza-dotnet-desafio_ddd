using System;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record VehicleScheduleSimulationInput
    {
        [JsonPropertyName("VeiculoId")]
        public int VehicleId { get; set; }
        [JsonPropertyName("Horas")]
        public int Hours { get; set; }
    }
}
