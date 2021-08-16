using System;

namespace App.Controllers
{
    public class ForbiddenExcepion : EndpointException
    {
        public ForbiddenExcepion() {}
        public ForbiddenExcepion(string message) : base(message) {}
        public ForbiddenExcepion(string message, Exception innerException) : base(message, innerException) {}
    }
}