using System;

namespace Infrastructure.Services
{
    [Serializable]
    public class EntityEmptyId : Exception
    {
        public EntityEmptyId(string message) : base(message) { }
    }
}