using System;
using System.Text.Json;
using System.Threading.Tasks;
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

        public ChatHub(IUserService userService)
        {
            this.UserService = userService;
        }

        public async Task Send(string toUserIdentifier, string text)
        {
            User userFrom = await this.UserService.GetUserAsync(new Guid(this.Context.UserIdentifier));
            ValidateUser(userFrom);

            User userTo = await this.UserService.GetUserAsync(new Guid(toUserIdentifier));
            ValidateUser(userTo);

            var message = new Message() { User = userFrom, Text = text };
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
    }
}