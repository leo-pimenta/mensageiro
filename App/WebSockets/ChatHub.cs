using System;
using System.Text.Json;
using System.Threading.Tasks;
using App.Proxies;
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

        public ChatHub(IUserService userService, IMessageWriter messageWriter, IMessageReader messageReader)
        {
            this.UserService = userService;
            this.MessageWriter = messageWriter;
            this.MessageReader = messageReader;
        }        

        public async Task Send(string toUserIdentifier, string text)
        {
            User userFrom = await this.UserService.GetUserAsync(new Guid(this.Context.UserIdentifier));
            ValidateUser(userFrom);

            User userTo = await this.UserService.GetUserAsync(new Guid(toUserIdentifier));
            ValidateUser(userTo);

            // TODO change SentAt to come from client?
            this.MessageWriter.Insert(userFrom, userTo, text, DateTime.UtcNow);
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