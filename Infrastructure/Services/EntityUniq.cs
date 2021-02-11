using System;

namespace Infrastructure.Services
{
    [Serializable]
    public class EntityUniq : Exception
    {
        public EntityUniq(string message) : base(message) { }
    }
}