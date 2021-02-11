using System;

namespace Domain.Entities
{
	public class User : IPerson, IUser
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Document { get; set; }
		public string Password { get; set; }
		public DateTime Birthday { get; set; }
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