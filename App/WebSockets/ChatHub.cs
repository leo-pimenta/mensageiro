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
        private readonly IMessageService MessageService;

        public ChatHub(IUserService userService, IMessageService messageService)
        {
            this.UserService = userService;
            this.MessageService = messageService;
            this.MessageService.OnMessagesReceivedAsync += OnMessageReceivedAsync;
        }        

        public async Task Send(string toUserIdentifier, string text)
        {
            User userFrom = await this.UserService.GetUserAsync(new Guid(this.Context.UserIdentifier));
            ValidateUser(userFrom);

            User userTo = await this.UserService.GetUserAsync(new Guid(toUserIdentifier));
            ValidateUser(userTo);

            // TODO change SentAt to come from client?
            var message = new Message() { User = userFrom, Text = text, SentAt = DateTime.Now };
            string jsonMessage = JsonSerializer.Serialize(message);
            
            await Clients.User(userTo.Guid.ToString()).SendAsync("OnMessage", jsonMessage);
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
            this.MessageService.Subscribe(this.Context.UserIdentifier);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            this.MessageService.Unsubscribe(this.Context.UserIdentifier);
            return Task.CompletedTask;
        }

        private async Task OnMessageReceivedAsync(Guid userIdentifier, MessageProxy messageProxy)
        {
            IClientProxy client = this.Clients.User(userIdentifier.ToString());

            if (client != null)
            {
                Message message = await messageProxy.RequestAsync();
                string json = JsonSerializer.Serialize(message);
                await client.SendAsync("OnMessage", json);
            }
        }
    }
}