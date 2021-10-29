using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Dtos;
using App.Services;
using Domain;
using Infra.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("/messages")]
    [Authorize]
    public class MessageController : AppControllerBase
    {
        private readonly IMessageService MessageService;

        public MessageController(
            IMessageService messageService,
            IResponseFactory responseFactory) : base(responseFactory) 
        {
            this.MessageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetChatMessages(Guid groupId, int page = 1)
        {
            ValidateGuid(groupId);
            var userId = new Guid(base.GetUserIdentifier());
            
            if (!await this.MessageService.IsUserInGroupAsync(userId, groupId))
            {
                throw new ForbiddenExcepion();
            }

            IEnumerable<Message> messages = await this.MessageService.GetMessagesAsync(groupId, page);
            var dto = new ChatMessagesResponseDto(userId, groupId, messages, page);
            return Ok(this.ResponseFactory.Create(dto));
        }

        private void ValidateGuid(Guid guid)
        {
            if (guid == default(Guid))
            {
                throw new BadRequestException("Invalid from user.");
            }
        }
    }
}