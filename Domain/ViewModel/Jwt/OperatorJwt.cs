
using System;
using System.Text.Json.Serialization;

namespace Domain.ViewModel.Jwt
{
    public record OperatorJwt
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Nome")]
        public string Name { get; set; }
        [JsonPropertyName("Matriculo")]
        public string Registration { get; set; }
        [JsonPropertyName("TipoDeAcesso")]
        public string Role { get; set; }
        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }
}
