using System;
using Infra;

namespace App.Controllers
{
    public class EndpointException : AppException
    {
        public EndpointException() {}
        public EndpointException(string message) : base(message) {}
        public EndpointException(string message, Exception innerException) : base(message, innerException) {}
    }
}