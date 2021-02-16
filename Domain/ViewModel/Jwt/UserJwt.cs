
using System;
using System.Text.Json.Serialization;

namespace Domain.ViewModel.Jwt
{
    public record UserJwt
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Nome")]
        public string Name { get; set; }
        [JsonPropertyName("CPF")]
        public string CPF { get; set; }
        [JsonPropertyName("TipoDeAcesso")]
        public string Role { get; set; }
        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }
}
