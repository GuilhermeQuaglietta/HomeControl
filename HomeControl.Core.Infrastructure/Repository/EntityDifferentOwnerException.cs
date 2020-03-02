using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HomeControl.Core.Infrastructure.Implementation
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
