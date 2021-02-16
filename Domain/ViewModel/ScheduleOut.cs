using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record ScheduleOut
    {
        public int VehicleId { get; set; }
        public double RentalHours { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpectedCollective { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public double HourlyValue { get; set; }
        public double Subtotal { get; set; }
        public string RentalPaymentReceipt { get; set; }
    }
}
