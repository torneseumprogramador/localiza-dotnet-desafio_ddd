using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("address")]
    public class Address
    {
        [Key]
        [Column]
		[JsonIgnore]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Column]
        [Required]
        [MaxLength(10)]
        [JsonPropertyName("Cep")]
        public string ZipCode { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("Logradouro")]
        public string Street { get; set; }

        [Column]
        [Required]
        [JsonPropertyName("Numero")]
        public int Number { get; set; }

        [Column]
        [JsonPropertyName("Complemento")]
        public string Complement { get; set; }

        [Column]
        [Required]
        [MaxLength(50)]
        [JsonPropertyName("Cidade")]
        public string City { get; set; }

        [Column]
        [Required]
        [MaxLength(2)]
        [JsonPropertyName("Estado")]
        public string State { get; set; }
    }
}
