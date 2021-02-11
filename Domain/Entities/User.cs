using System;
using Infrastructure.Database;

namespace Domain.Entities
{
    [Table(Name = "users")]
	public class User : IPerson, IUser
	{
		[Pk]
		public int Id { get; set; }
		[DbField]
		public string Name { get; set; }
		[DbField]
		public string Document { get; set; }
		[DbField]
		public string Password { get; set; }
		[DbField]
		public int Type { get; set; }
		[DbField]
		public DateTime Birthday { get; set; }
		[DbField]
		public int IdAddress { get; set; }

		public string CPF
		{
			get
			{
				return this.Document;
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