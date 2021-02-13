using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("brands")]
    public class Brand
    {
        [Key]
        [Column]
		[JsonIgnore]
        public int Id { get; set; }

        [Column]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
