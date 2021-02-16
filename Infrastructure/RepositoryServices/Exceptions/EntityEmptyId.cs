using System;

namespace Infrastructure.RepositoryServices.Exceptions
{
    [Serializable]
    public class EntityEmptyId : Exception
    {
        public EntityEmptyId(string message) : base(message) { }
    }
}