using System;
using System.Text.Json.Serialization;
using Domain.Entities;

namespace Domain.ViewModel
{
    public record VehicleScheduleSimulationOutput
    {
        [JsonPropertyName("Veiculo")]
        public VehicleMap Vehicle { get; set; }
        [JsonPropertyName("DataHoraInicial")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("DataHoraFinal")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("Total")]
        public double Total { get; set; }
    }
}
