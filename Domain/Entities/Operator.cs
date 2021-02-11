using System;

namespace Domain.Entities
{
    public class Operator : IPerson, IOperator
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Registration { get; set; }
        public string Password { get; set; }
    }
}
