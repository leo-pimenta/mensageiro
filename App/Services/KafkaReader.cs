using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Proxies;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace App.Services
{
    public class KafkaReader : IMessageReader
    {        
        private readonly IConsumer<Guid, string> Consumer;
        private readonly IUserService UserService;
        private readonly ISendCommand SendCommand;
        private readonly IConfiguration Configuration;
        private Task ConsumerTask;
        private CancellationTokenSource ConsumerCancelationToken;

        public KafkaReader(
            IConsumer<Guid, string> consumer, 
            IUserService userService,
            ISendCommand sendCommand,
            IConfiguration configuration)
        {
            this.Consumer = consumer;
            this.UserService = userService;
            this.SendCommand = sendCommand;
            this.Configuration = configuration;
        }

        // TODO there is possibly a need to refactor this and change it to work with consumers for each user.
        public void Subscribe(string userIndentifier)
            => this.Consumer.Subscribe(
                this.Configuration.GetArray("broker:topics")
                    .Select(topic => topic.Replace("{userIdentifier}", userIndentifier)));

        // TODO there is possibly a need to refactor this and change it to work with consumers for each user.
        public void Unsubscribe(string userIndentifier) 
        {
            foreach (string topicName in this.Configuration.GetArray("broker:topics"))
            {
                string userIdApliedTopicName = topicName.Replace("{userIdentifier}", userIndentifier);
                this.Consumer.Subscription.Remove(userIdApliedTopicName);
            }
        }

        public void Start()
        {
            this.ConsumerTask = Task.Run(async () => await ConsumeAsync());
            this.ConsumerCancelationToken = new CancellationTokenSource();
        }

        public void Stop()
        {
            this.ConsumerCancelationToken.Cancel();
        }

        private async Task ConsumeAsync()
        {
            while (true)
            {
                if (this.ConsumerCancelationToken.IsCancellationRequested)
                {
                    break;
                }

                try
                {
                    ConsumeResult<Guid, string> result = this.Consumer.Consume(TimeSpan.FromSeconds(10));

                    if (result != null)
                    {
                        Message<Guid, string> message = result.Message;

                        var userProxy = new UserProxy(message.Key, this.UserService);
                        var messageProxy = new MessageProxy(message.Value, message.Timestamp.UtcDateTime, userProxy);
                        await this.SendCommand.SendAsync(message.Key, messageProxy);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}