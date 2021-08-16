using System;
using Infra;

namespace App.Services
{
    public class ContactInvitationNotFoundException : AppException
    {
        public ContactInvitationNotFoundException() {}
        public ContactInvitationNotFoundException(string message) : base(message) {}
        public ContactInvitationNotFoundException(string message, Exception innerException) : base(message, innerException) {}
    }
}