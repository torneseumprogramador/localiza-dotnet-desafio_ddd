using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("models")]
    public class Model
    {
        [Key]
        [Column]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Column]
        [Required]
        [MaxLength(50)]
        [JsonPropertyName("Nome")]
        public string Name { get; set; }
    }
}
