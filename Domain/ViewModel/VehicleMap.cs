using System;
using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.ViewModel
{
    public record VehicleMap
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Marca")]
        public Brand Brand { get; set; }

        [JsonPropertyName("Modelo")]
        public Model Model { get; set; }

        [JsonPropertyName("ValorHora")]
        public double HourValue { get; set; }

        [JsonPropertyName("Ano")]
        public int Year { get; set; }

        [JsonPropertyName("Placa")]
        public string LicensePlate { get; set; }

        [JsonPropertyName("CapacidadePortaMalas")]
        public int LuggageCapacity { get; set; }

        [JsonPropertyName("CapacidadeTanque")]
        public TankCapacity TankCapacity { get; set; }

        [JsonPropertyName("Categoria")]
        public Category Category { get; set; }
    }
}
