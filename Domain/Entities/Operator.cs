using System;

namespace Domain.Entities
{
    public class Operator : IPerson, IOperator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
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
