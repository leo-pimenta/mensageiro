using System;
using System.Threading.Tasks;
using Domain;

namespace App.Proxies
{
    public class MessageProxy : IProxy<Message>
    {
        private Message Message;
        private IProxy<User> UserProxy;

        public MessageProxy(string text, DateTime sentAt, IProxy<User> userProxy)
        {
            this.Message = new Message()
            {
                SentAt = sentAt,
                Text = text,
                User = null
            };

            this.UserProxy = userProxy;
        }

        public Message Request()
        {
            if (this.Message.User == null)
            {
                this.Message.User = this.UserProxy.Request();
            }

            return this.Message;
        }

        public async Task<Message> RequestAsync()
        {
            if (this.Message.User == null)
            {
                this.Message.User = await this.UserProxy.RequestAsync();
            }

            return this.Message;
        }
    }
}