using System;

namespace App.Services
{
    internal class UserNotInGroupException : Exception
    {
        public UserNotInGroupException() {}
        public UserNotInGroupException(string message) : base(message) {}
        public UserNotInGroupException(string message, Exception innerException) 
            : base(message, innerException) {}
    }
}