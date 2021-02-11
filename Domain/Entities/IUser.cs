using System;

namespace Domain.Entities
{
    public interface IUser
    {
        string CPF {get; set;}
        DateTime Birthday {get; set;}
        int IdAddress {get; set;}
    }
}
