using System;

namespace Rentx.Web.Common.Exceptions
{
    public class RentxValidationException : Exception
    {
        public RentxValidationException() : base()
        {
        }

        public RentxValidationException(string message) : base(message)
        {
        }

        public RentxValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
