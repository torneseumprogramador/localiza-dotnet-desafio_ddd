using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("vehicles")]
    public class Vehicle
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Column]
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Column]
        [Required]
        [MaxLength(50)]
        public string LicensePlate { get; set; }

        [Column]
        [Required]
        [MaxLength(50)]
        public DateTime Date { get; set; }

        [Column]
        [Required]
        [MaxLength(50)]
        public int LuggageCapacity { get; set; }

        [Column]
        [Required]
        public TankCapacity TankCapacity { get; set; }

        [Column]
        [Required]
        public int IdCategory { get; set; }

        public Category Category { get; set; }

    }
}
