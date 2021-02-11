using System;

namespace Domain.Entities
{
    public interface IPerson
    {
        int Id {get; set;}
        string Name {get; set;}
        string Password {get; set;}
        string Document {get; set; }
        PersonRole Role {get;}
    }
}
