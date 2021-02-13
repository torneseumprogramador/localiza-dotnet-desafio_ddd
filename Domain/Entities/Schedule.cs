using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("schedules")]
    public class Schedule
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Column]
        [Required]
        public int VehicleId { get; set; }

        [Column]
        [Required]
        public int UserId { get; set; }

        [Column]
        [Required]
        public int OparatorId { get; set; }

        [Column]
        [Required]
        public DateTime Date { get; set; }

        [Column]
        [Required]
        public DateTime ExpectedCollective { get; set; }

        [Column]
        [Required]
        public DateTime CollectiveHeld { get; set; }

        [Column]
        [Required]
        public DateTime EstimatedDeliveryTime { get; set; }

        [Column]
        [Required]
        public DateTime DeliveryCompleted { get; set; }

        [Column]
        [Required]
        public double HourlyValue { get; set; }

        [Column]
        [Required]
        public double RentalHours { get; set; }

        [Column]
        [Required]
        public double Subtotal { get; set; }

        [Column]
        [Required]
        public double AdditionalCosts { get; set; }

        [Column]
        [Required]
        public double Total { get; set; }

        [Column]
        [Required]
        public double SurveyCarriedOut { get; set; }
    }
}
