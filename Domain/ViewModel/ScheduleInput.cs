using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record ScheduleInput
    {
        [JsonPropertyName("VeiculoId")]
        public int VehicleId { get; set; }
        [JsonPropertyName("UsuarioId")]
        public int UserId { get; set; }
        [JsonPropertyName("OperadorId")]
        public int OparatorId { get; set; }
        [JsonPropertyName("HorasLocacao")]
        public double RentalHours { get; set; }
    }
}
