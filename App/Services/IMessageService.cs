using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace App.Services
{
    public interface IMessageService
    {
        Task<bool> IsUserInGroupAsync(Guid userId, Guid groupId);
        Task<IEnumerable<Message>> GetMessagesAsync(Guid groupId, int page = 1);
        Task SendMessageAsync(Guid groupId, Guid userId, string text, DateTime date);
    }
}