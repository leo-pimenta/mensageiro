using System;
using System.Threading.Tasks;
using App.Controllers;
using App.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace App.WebSockets
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUserService UserService;
        private readonly IMessageWriter MessageWriter;
        private readonly IMessageReader MessageReader;
        private readonly IMessageService MessageService;

        public ChatHub(IUserService userService, IMessageWriter messageWriter, IMessageReader messageReader,
            IMessageService messageService)
        {
            this.UserService = userService;
            this.MessageWriter = messageWriter;
            this.MessageReader = messageReader;
            this.MessageService = messageService;
        }        

        public async Task Send(string groupIdentifier, string text)
        {
            var groupId = new Guid(groupIdentifier);
            var userId = new Guid(this.Context.UserIdentifier);
            
            try 
            {
                await this.MessageService.SendMessageAsync(groupId, userId, text, DateTime.UtcNow);
            }
            catch (UserNotInGroupException)
            {
                throw new ForbiddenException();
            }
        }

        public void ValidateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
        }

        public override Task OnConnectedAsync()
        {
            this.MessageReader.Subscribe(this.Context.UserIdentifier);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            this.MessageReader.Unsubscribe(this.Context.UserIdentifier);
            return Task.CompletedTask;
        }
    }
}