using System;
using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.ViewModel
{
    public record VehicleMap
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Brand Brand { get; set; }

        public Model Model { get; set; }

        public double HourValue { get; set; }

        public int Year { get; set; }

        public string LicensePlate { get; set; }

        public int LuggageCapacity { get; set; }

        public TankCapacity TankCapacity { get; set; }

        public Category Category { get; set; }
    }
}
