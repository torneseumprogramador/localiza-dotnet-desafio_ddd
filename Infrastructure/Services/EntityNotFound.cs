using System;

namespace Infrastructure.Services
{
    [Serializable]
    public class EntityNotFound : Exception
    {
        public EntityNotFound(string message) : base(message) { }
    }
}