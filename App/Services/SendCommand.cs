using System;
using System.Text.Json;
using System.Threading.Tasks;
using App.Proxies;
using App.WebSockets;
using Domain;
using Microsoft.AspNetCore.SignalR;

namespace App.Services
{
    public class SignalRSendCommand : ISendCommand
    {
        private readonly IHubContext<ChatHub> HubContext;

        public SignalRSendCommand(IHubContext<ChatHub> hubContext)
        {
            this.HubContext = hubContext;
        }

        public async Task SendAsync(Guid toUserIdentifier, MessageProxy messageProxy)
        {
            IClientProxy client = this.HubContext.Clients.User(toUserIdentifier.ToString());

            if (client != null)
            {
                Message message = await messageProxy.RequestAsync();
                string json = JsonSerializer.Serialize(message);
                await client.SendAsync("OnMessage", json);
            }
        }
    }
}