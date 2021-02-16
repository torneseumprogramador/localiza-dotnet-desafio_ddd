using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record OperatorSave
    {
        [Required]
        [JsonPropertyName("Nome")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("Senha")]
        public string Password { get; set; }
        [Required]
        [JsonPropertyName("Matricula")]
        public string Registration { get; set; }
    }
}
