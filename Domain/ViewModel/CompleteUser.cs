using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record CompleteUser
    {
        [Required]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("Senha")]
        public string Password { get; set; }

        [Required]
        [JsonPropertyName("DataAniversario")]
        public DateTime Birthday { get; set; }

        [Required]
        [JsonPropertyName("Documento")]
        public string Document { get; set; }

        [Required]
        [JsonPropertyName("EnderecoId")]
        public int AddressId { get; set; }

        [Required]
        [JsonPropertyName("Cep")]
        public string ZipCode { get; set; }

        [Required]
        [JsonPropertyName("Logradouro")]
        public string Street { get; set; }

        [Required]
        [JsonPropertyName("Numero")]
        public int Number { get; set; }

        [JsonPropertyName("Complemento")]
        public string Complement { get; set; }

        [Required]
        [JsonPropertyName("Cidade")]
        public string City { get; set; }

        [Required]
        [JsonPropertyName("Estado")]
        public string State { get; set; }
    }
}
