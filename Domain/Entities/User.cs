using System;

namespace Infrastructure.Entities
{
	public class User : IPerson, IUser
	{
		public int Id { get; set; }
		public int Name { get; set; }
    	public string CPF { get; set; }
		public string Password  { get; set; }
		public DateTime Birthday  { get; set; }
        public int IdAddress {get; set;}
    }
}
