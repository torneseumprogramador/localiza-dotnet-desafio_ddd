
using System;
using System.Text.Json.Serialization;

namespace Domain.ViewModel.Jwt
{
    public record PersonJwt
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Nome")]
        public string Name { get; set; }
        [JsonPropertyName("Documento")]
        public string Document { get; set; }
        [JsonPropertyName("TipoDeAcesso")]
        public string Role { get; set; }
        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }
}
