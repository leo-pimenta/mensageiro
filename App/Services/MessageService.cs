using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using Infra.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class MessageService : DbService, IMessageService
    {
        private readonly IMessages Messages;
        private readonly IChatGroups ChatGroups;

        public MessageService(MsgContext context, IMessages messages, IChatGroups chatGroups) : base(context)
        {
            this.Messages = messages;
            this.ChatGroups = chatGroups;
        }

        public async Task<bool> IsUserInGroupAsync(Guid userId, Guid groupId) => 
            await this.ChatGroups.GetGroupRelationshipAsync(userId, groupId) is not null;

        public async Task<IEnumerable<Message>> GetMessagesAsync(Guid groupId, int page = 1)
        {
            const int PageSize  = 50;
            return await this.Messages.GetPaginatedMessages(page, PageSize, groupId).ToListAsync();
        }
    }
}