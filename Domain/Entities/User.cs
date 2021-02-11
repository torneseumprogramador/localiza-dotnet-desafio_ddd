using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
	[Table("users")]
	public class User : IPerson, IUser
	{
		[Key]
		[Column]
		public int Id { get; set; }

		[Required]
		[Column]
		public string Name { get; set; }

		[Required]
		[Column]
		[JsonIgnore]
		public string Document { get; set; }

		[Required]
		[Column]
		public string Password { get; set; }

		[JsonIgnore]
		[Column]
		public int Type { get; set; }

		[Required]
		[Column]
		public DateTime Birthday { get; set; }

		[Column]
		[JsonIgnore]
		public int IdAddress { get; set; }

		[Required]
		public string CPF
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
				return PersonRole.User;
			}
		}
    }
}