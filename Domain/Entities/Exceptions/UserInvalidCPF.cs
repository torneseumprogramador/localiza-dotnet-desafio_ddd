using System;

namespace Domain.Entities.Exceptions
{
    [Serializable]
    public class UserInvalidCPF : Exception
    {
        public UserInvalidCPF(string message) : base(message) { }
    }
}