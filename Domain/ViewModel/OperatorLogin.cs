using System;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record OperatorLogin
    {
        [JsonPropertyName("Matricula")]
        public string Registration { get;set;}        
        [JsonPropertyName("Senha")]
        public string Password {get;set;}        
    }
}
