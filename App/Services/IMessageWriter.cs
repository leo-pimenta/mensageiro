using System;
using Domain;

namespace App.Services
{
    public interface IMessageWriter
    {
        void Insert(User userFrom, User userTo, string message, DateTime sentAt);   
    }
}