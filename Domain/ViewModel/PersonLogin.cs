using System;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record PersonLogin
    {
        [JsonPropertyName("Documento")]
        public string Document { get; set; }
        [JsonPropertyName("Senha")]
        public string Password { get; set; }
    }
}
