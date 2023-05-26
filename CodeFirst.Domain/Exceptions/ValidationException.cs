using System;
using System.Collections.Generic;

namespace CodeFirst.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("Se han producido uno o más errores de validación.")
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; }

        public ValidationException(List<string> failures)
            : this()
        {
            Errors = failures;
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}