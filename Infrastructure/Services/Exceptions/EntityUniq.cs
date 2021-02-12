using System;

namespace Infrastructure.Services.Exceptions
{
    [Serializable]
    public class EntityUniq : Exception
    {
        public EntityUniq(string message) : base(message) { }
    }
}