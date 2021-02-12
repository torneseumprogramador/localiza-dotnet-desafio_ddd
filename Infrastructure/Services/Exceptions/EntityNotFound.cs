using System;

namespace Infrastructure.Services.Exceptions
{
    [Serializable]
    public class EntityNotFound : Exception
    {
        public EntityNotFound(string message) : base(message) { }
    }
}