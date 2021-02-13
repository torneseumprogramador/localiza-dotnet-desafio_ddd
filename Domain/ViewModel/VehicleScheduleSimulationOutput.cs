using System;
using Domain.Entities;

namespace Domain.ViewModel
{
    public record VehicleScheduleSimulationOutput
    {
        public VehicleMap Vehicle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Total { get; set; }
    }
}
