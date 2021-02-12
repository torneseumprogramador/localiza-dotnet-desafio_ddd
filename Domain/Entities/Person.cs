
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("users")]
    public class Person : IPerson
    {
        [Key]
        [Column]
        public virtual int Id { get; set; }

        [Column]
		[Required]
        public virtual string Name { get; set; }

        [Column]
		[Required]
        public virtual string Document { get; set; }

        [Column]
		[Required]
        public virtual int Type { get; set; }

        [Column]
		[Required]
        public virtual string Password { get; set; }

        [Column]
        public virtual int? IdAddress { get; set; }

        public virtual PersonRole Role { get { return (PersonRole)Enum.ToObject(typeof(PersonRole), this.Type); } }
    }
}
