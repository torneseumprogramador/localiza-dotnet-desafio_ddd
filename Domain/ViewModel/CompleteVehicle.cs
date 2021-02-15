using System;
using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.ViewModel
{
    public record CompleteVehicle
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BrandId { get; set; }
        public string Brand { get; set; }

        public int ModelId { get; set; }
        public string Model { get; set; }

        public int CategoryId { get; set; }
        public string Category { get; set; }

        public int Year { get; set; }

        public string LicensePlate { get; set; }

        public double HourValue { get; set; }

        public int LuggageCapacity { get; set; }

        public TankCapacity TankCapacity { get; set; }
    }
}
