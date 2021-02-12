using System;

namespace Infrastructure.Services.Exceptions
{
    [Serializable]
    public class EntityEmptyId : Exception
    {
        public EntityEmptyId(string message) : base(message) { }
    }
}