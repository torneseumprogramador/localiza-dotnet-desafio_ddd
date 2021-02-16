using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("schedules")]
    public class Schedule
    {
        [Key]
        [Column]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("VeiculoId")]
        public int VehicleId { get; set; }

        [Column]
        [Required]
        [JsonIgnore]
        [JsonPropertyName("Veiculo")]
        public Vehicle Vehicle { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("UsuarioId")]
        public int UserId { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("OperadorId")]
        public int OparatorId { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("Data")]
        public DateTime Date { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("DataHoraColetaPrevista")]
        public DateTime ExpectedCollective { get; set; }

        [Column]
        [JsonPropertyName("DataHoraColetaRealizada")]
        public DateTime CollectiveHeld { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("DataHoraEntregaPrevista")]
        public DateTime EstimatedDeliveryTime { get; set; }

        [Column]
        [JsonPropertyName("DataHoraEntregaRealizada")]
        public DateTime DeliveryCompleted { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("ValorHora")]
        public double HourlyValue { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("HorasLocacao")]
        public double RentalHours { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("SubTotal")]
        public double Subtotal { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("CustosAdicionais")]
        public double AdditionalCosts { get; set; }

        [Column]
        [JsonPropertyName("ValorTotal")]
        public double Total { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("VistoriaRealizada")]
        public bool SurveyPerformed { get; set; }

        [JsonPropertyName("Checkllist")]
        public Checklist Checklist { get; set; }
    }
}
