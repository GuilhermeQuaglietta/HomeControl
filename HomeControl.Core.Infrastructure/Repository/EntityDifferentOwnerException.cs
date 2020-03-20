using System;
using System.Runtime.Serialization;

namespace HomeControl.Core.Infrastructure.Repository
{
    [Serializable]
    public class EntityDifferentOwnerException : Exception
    {
        public EntityDifferentOwnerException(string message) : base(message)
        {
        }

        public EntityDifferentOwnerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EntityDifferentOwnerException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
