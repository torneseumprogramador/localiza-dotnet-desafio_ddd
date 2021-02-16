using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities.Enums;

namespace Domain.Entities
{
    [Table("vehicles")]
    public class Vehicle
    {
        [Key]
        [Column]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Column]
        [Required]
        [MaxLength(150)]
        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("MarcaId")]
        public int BrandId { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("ModeloId")]
        public int ModelId { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("Ano")]
        public int Year { get; set; }

        [Column]
        [Required]
        [MaxLength(50)]
        [JsonPropertyName("Placa")]
        public string LicensePlate { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("ValorHora")]
        public double HourValue { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("CapacidadePortaMalas")]
        public int LuggageCapacity { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("CapacidadeTaque")]
        public TankCapacity TankCapacity { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("CategoriaId")]
        public int CategoryId { get; set; }
    }
}
