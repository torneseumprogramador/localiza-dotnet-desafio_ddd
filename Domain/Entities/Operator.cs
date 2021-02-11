using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("users")]
    public class Operator : IPerson, IOperator
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Column]
		[Required]
        public string Name { get; set; }

        [Column]
		[JsonIgnore]
        public string Document { get; set; }

        [Column]
		[JsonIgnore]
        public int Type { get; set; }

        [Column]
		[Required]
        public string Password { get; set; }

		[Required]
        public string Registration
        {
            get
            {
                return this.Document;
            }
            set
            {
                this.Document = value;
            }
        }

        public PersonRole Role
        {
            get
            {
                return PersonRole.Operator;
            }
        }
    }
}
