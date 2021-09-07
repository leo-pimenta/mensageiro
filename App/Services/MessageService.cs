using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork UnitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public async Task<bool> IsUserInGroupAsync(Guid userId, Guid groupId)
        {
            return await this.UnitOfWork.ExecuteAsync(async context => 
                await context.UserGroupRelationships.FindAsync(userId, groupId) is not null);
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(Guid groupId, int page = 1)
        {
            const int PageSize  = 50;

            return await this.UnitOfWork.ExecuteAsync(async context => 
                await context.Messages
                    .Where(message => message.GroupId == groupId)
                    .OrderBy(message => message.Id)
                    .TakeLast(PageSize)
                    .ToListAsync());
        }
    }
}