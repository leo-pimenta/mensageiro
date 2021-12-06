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
        private readonly IMessageWriter MessageWriter;
        private readonly IUserService UserService;

        public MessageService(MsgContext context, IMessages messages, IChatGroups chatGroups, 
            IMessageWriter messageWriter, IUserService userService) 
            : base(context)
        {
            this.Messages = messages;
            this.ChatGroups = chatGroups;
            this.MessageWriter = messageWriter;
            this.UserService = userService;
        }

        public async Task<bool> IsUserInGroupAsync(Guid userId, Guid groupId) => 
            await this.ChatGroups.GetGroupRelationshipAsync(userId, groupId) is not null;

        public async Task<IEnumerable<Message>> GetMessagesAsync(Guid groupId, int page = 1)
        {
            const int PageSize  = 50;
            return await this.Messages.GetPaginatedMessages(page, PageSize, groupId).ToListAsync();
        }

        public async Task SendMessageAsync(Guid groupId, Guid userId, string text, DateTime date)
        {
            UserGroupRelationship relationship = await GetGroupForUserAsync(userId, groupId);            
            var message = new Message(relationship.GroupId, relationship.UserId, text, date);
            this.Messages.Add(message);
            await SaveDbChangesAsync();
            this.MessageWriter.Insert(relationship.UserId, relationship.GroupId, text, DateTime.UtcNow);
        }

        private async Task<UserGroupRelationship> GetGroupForUserAsync(Guid userId, Guid groupId)
        {
            var relationship = await this.ChatGroups.GetGroupRelationshipAsync(userId, groupId);

            if (relationship == null)
            {
                throw new UserNotInGroupException("The user is not on the requested group.");
            }
            
            return relationship;
        }
    }
}