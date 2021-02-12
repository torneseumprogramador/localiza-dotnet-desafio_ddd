using System;

namespace Domain.Entities
{
    public interface IUser
    {
        string CPF { get; }
        DateTime Birthday { get; set;}
        int? IdAddress {get; set;}
    }
}
