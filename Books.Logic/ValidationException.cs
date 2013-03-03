using System;

namespace Books.Logic
{
    public class ValidationException : ApplicationException
    {
         public ValidationException(string message) : base(message) { }
    }
}