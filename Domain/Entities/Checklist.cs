using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("checklists")]
    public class Checklist
    {
        [Key]
        [Column]
        [JsonIgnore]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("CarroLimpo")]
        public bool CleanCar { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("TanqueCheio")]
        public bool FullTank { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("TanqueLitroPendente")]
        public int PendingFullTank { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("Amassados")]
        public bool Wrinkled { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("Arranhoes")]
        public bool Scratches { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("AgendamentoId")]
        public int ScheduleId { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("OperadorId")]
        public int OperatorId { get; set; }
    }
}
