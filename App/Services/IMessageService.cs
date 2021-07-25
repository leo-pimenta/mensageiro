using System;
using System.Threading.Tasks;
using App.Proxies;
using Domain;

namespace App.Services
{
    public interface IMessageService
    {
        event Func<Guid, MessageProxy, Task> OnMessagesReceivedAsync;

        void Insert(User userFrom, User userTo, string message, DateTime sentAt);
        void Subscribe(string userIndentifier);
        void Unsubscribe(string userIndentifier);
        void Start();
    }
}