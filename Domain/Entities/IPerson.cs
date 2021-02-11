using System;

namespace Domain.Entities
{
    public interface IPerson
    {
        int Id {get; set;}
        int Name {get; set;}
        string Password {get; set;}
    }
}
