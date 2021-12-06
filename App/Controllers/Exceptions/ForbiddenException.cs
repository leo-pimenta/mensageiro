using System;

namespace App.Controllers
{
    public class ForbiddenException : EndpointException
    {
        public ForbiddenException() {}
        public ForbiddenException(string message) : base(message) {}
        public ForbiddenException(string message, Exception innerException) : base(message, innerException) {}
    }
}