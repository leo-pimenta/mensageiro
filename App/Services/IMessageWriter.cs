using System;
using Domain;

namespace App.Services
{
    public interface IMessageWriter
    {
        void Insert(Guid userId, Guid groupId, string message, DateTime sentAt);
        void InsertContactInvitation(ContactInvitation invitation, DateTime sentAt);
    }
}