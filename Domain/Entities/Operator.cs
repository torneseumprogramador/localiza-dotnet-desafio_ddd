using System;
using Infrastructure.Database;

namespace Domain.Entities
{
    [Table(Name = "users")]
    public class Operator : IPerson, IOperator
    {
        [Pk]
        public int Id { get; set; }
        [DbField]
        public string Name { get; set; }
        [DbField]
        public string Document { get; set; }
        [DbField]
        public int Type { get; set; }
        [DbField]
        public string Password { get; set; }

        public string Registration
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
                return PersonRole.Operator;
            }
        }
    }
}
