using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
	[Table("users")]
	public class User : Person, IUser
	{
		[Column]
		[JsonIgnore]
		public override string Document { get; set; }

		[JsonIgnore]
		[Column]
		public override int Type { get; set; }

		[Column]
		public DateTime Birthday { get; set; }

		[Column]
		[JsonIgnore]
		public override int? IdAddress { get; set; }

		[Column]
		public Address Address { get; set; }

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

		public override PersonRole Role
		{
			get
			{
				return PersonRole.User;
			}
		}
    }
}