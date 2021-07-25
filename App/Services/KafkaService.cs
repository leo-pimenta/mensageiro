using System;
using System.Threading;
using System.Threading.Tasks;
using App.Proxies;
using Confluent.Kafka;
using Domain;
using Microsoft.Extensions.Configuration;

namespace App.Services
{
    public class KafkaService : IMessageService, IDisposable
    {
        public event Func<Guid, MessageProxy, Task> OnMessagesReceivedAsync;

        private readonly IConsumer<Guid, string> Consumer;
        private readonly IProducer<Guid, string> Producer;
        private readonly IConfiguration Configuration;
        private readonly IUserService UserService;

        private Task ConsumerTask;
        private CancellationTokenSource ConsumerCancelationToken;

        public KafkaService(
            IConsumer<Guid, string> consumer, 
            IProducer<Guid, string> producer, 
            IConfiguration configuration, 
            IUserService userService)
        {
            this.Consumer = consumer;
            this.Producer = producer;
            this.Configuration = configuration;
            this.UserService = userService;
        }

        public void Insert(User userFrom, User userTo, string text, DateTime sentAt)
        {
            Action<DeliveryReport<Guid, string>> handler = r => 
            {
                string log = !r.Error.IsError 
                    ? $"Message succesfully inserted for user {userTo.Guid} on {r.TopicPartitionOffset}" 
                    : $"Error while inserting message for user {userTo.Guid}. Error: {r.Error.Reason}";

                Console.WriteLine(log);
            };

            var message = new Message<Guid, string>()
            { 
                Value = text,
                Key = userFrom.Guid,
                Timestamp = new Timestamp(sentAt)
            };

            this.Producer.Produce("messages-user-{userTo.Guid}", message, handler);
            this.Producer.Flush(TimeSpan.FromSeconds(10));
        }

        public void Subscribe(string userIndentifier)
            => this.Consumer.Subscribe($"messages-user-{userIndentifier}");

        public void Unsubscribe(string userIndentifier) 
            => this.Consumer.Subscription.Remove($"messages-user-{userIndentifier}");

        public void Dispose()
        {
            this.Producer?.Dispose();
            this.Consumer?.Dispose();
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

                ConsumeResult<Guid, string> result = this.Consumer.Consume(TimeSpan.FromSeconds(10));
                Message<Guid, string> message = result.Message;

                var userProxy = new UserProxy(message.Key, this.UserService);
                var messageProxy = new MessageProxy(message.Value, message.Timestamp.UtcDateTime, userProxy);
                await this.OnMessagesReceivedAsync(message.Key, messageProxy);
            }
        }
    }
}