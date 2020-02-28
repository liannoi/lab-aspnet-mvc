using System;
using System.Runtime.Serialization;

namespace HumanResources.WebUI.Areas.Exceptions
{
    public class InvalidModelStateException : Exception
    {
        public InvalidModelStateException()
        {
        }

        public InvalidModelStateException(string message) : base(message)
        {
        }

        public InvalidModelStateException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidModelStateException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}