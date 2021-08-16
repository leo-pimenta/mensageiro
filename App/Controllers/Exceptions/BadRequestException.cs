using System;

namespace App.Controllers
{
    public class BadRequestException : EndpointException
    {
        public BadRequestException() {}
        public BadRequestException(string message) : base(message) {}
        public BadRequestException(string message, Exception innerException) : base(message, innerException) {}
    }
}