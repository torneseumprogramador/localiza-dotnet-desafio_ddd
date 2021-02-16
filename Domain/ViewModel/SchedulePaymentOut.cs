using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities;

namespace Domain.ViewModel
{
    public record SchedulePaymentOut
    {
        [JsonPropertyName("Agendamento")]
        public Schedule Schedule { get; set; }
        [JsonPropertyName("PagamentoPrevisto")]
        public string Invoice { get; set; }
    }
}
