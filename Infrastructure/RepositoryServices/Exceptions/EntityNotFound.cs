using System;

namespace Infrastructure.RepositoryServices.Exceptions
{
    [Serializable]
    public class EntityNotFound : Exception
    {
        public EntityNotFound(string message) : base(message) { }
    }
}