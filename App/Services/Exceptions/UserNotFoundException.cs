using System;
using Infra;

namespace App.Services
{
    public class UserNotFoundException : AppException
    {
        public UserNotFoundException() {}
        public UserNotFoundException(string message) : base(message) {}
        public UserNotFoundException(string message, Exception innerException) : base(message, innerException) {}
    }
}