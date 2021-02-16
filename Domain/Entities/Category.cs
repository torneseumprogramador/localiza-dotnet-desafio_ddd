using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Domain.Entities
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Column]
        [Required]
        [MaxLength(100)]
        [JsonPropertyName("Nome")]
        public string Name { get; set; }
    }
}
