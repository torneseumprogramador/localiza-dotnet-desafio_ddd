using System;

namespace Domain.ViewModel
{
    public record VehicleScheduleSimulationInput
    {
      public int VehicleId { get; set; }    
      public int Hours { get; set; }      
    }
}
