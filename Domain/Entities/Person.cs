using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Infrastructure.Database;

namespace Domain.Entities
{
    [Table("users")]
    public class Person : IPerson
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Column]
		[Required]
        public string Name { get; set; }

        [Column]
		[Required]
        public string Document { get; set; }

        [Column]
		[Required]
        public int Type { get; set; }

        [Column]
		[Required]
        public string Password { get; set; }

        public PersonRole Role { get { return (PersonRole)Enum.ToObject(typeof(PersonRole), this.Type); } }
    }
}
