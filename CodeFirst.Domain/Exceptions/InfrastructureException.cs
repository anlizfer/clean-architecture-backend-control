using System;
using System.Globalization;

namespace CodeFirst.Domain.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException() : base()
        {
        }

        public InfrastructureException(string message) : base(message)
        {
        }

        public InfrastructureException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public InfrastructureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}