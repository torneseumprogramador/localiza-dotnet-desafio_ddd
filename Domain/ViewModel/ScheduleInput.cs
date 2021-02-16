using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record ScheduleInput
    {
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public int OparatorId { get; set; }
        public double RentalHours { get; set; }
    }
}
