using System;
using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.ViewModel
{
    public record CompleteVehicle
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        [JsonPropertyName("MarcaId")]
        public int BrandId { get; set; }
        [JsonPropertyName("Marca")]
        public string Brand { get; set; }

        [JsonPropertyName("ModeloId")]
        public int ModelId { get; set; }
        [JsonPropertyName("Modelo")]
        public string Model { get; set; }

        [JsonPropertyName("CategoriaId")]
        public int CategoryId { get; set; }
        [JsonPropertyName("Cagtegoria")]
        public string Category { get; set; }

        [JsonPropertyName("Ano")]
        public int Year { get; set; }

        [JsonPropertyName("Placa")]
        public string LicensePlate { get; set; }

        [JsonPropertyName("ValorHora")]
        public double HourValue { get; set; }

        [JsonPropertyName("CapacidadePortaMalas")]
        public int LuggageCapacity { get; set; }

        [JsonPropertyName("CapacidadeTanque")]
        public TankCapacity TankCapacity { get; set; }
    }
}
