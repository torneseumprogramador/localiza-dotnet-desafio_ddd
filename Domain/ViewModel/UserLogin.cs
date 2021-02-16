using System;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record UserLogin
    {
        [JsonPropertyName("CPF")]
        public string CPF { get; set; }
        [JsonPropertyName("Senha")]
        public string Password { get; set; }
    }
}
