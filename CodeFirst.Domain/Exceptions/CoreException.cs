using System;
using System.Globalization;

namespace CodeFirst.Domain.Exceptions
{
    public class CoreException : Exception
    {
        public CoreException() : base()
        {
        }

        public CoreException(string message) : base(message)
        {
        }

        public CoreException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public CoreException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}