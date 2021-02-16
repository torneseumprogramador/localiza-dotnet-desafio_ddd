using System;

namespace Infrastructure.RepositoryServices.Exceptions
{
    [Serializable]
    public class EntityUniq : Exception
    {
        public EntityUniq(string message) : base(message) { }
    }
}