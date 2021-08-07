using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Domain;
using Microsoft.Extensions.Configuration;

namespace App.Services
{
    public class KafkaWriter : IMessageWriter
    {
        private readonly IAdminClient Admin;
        private readonly IProducer<Guid, string> Producer;
        private readonly IConfiguration Configuration;
        private readonly IUserService UserService;

        public KafkaWriter(
            IAdminClient admin,
            IProducer<Guid, string> producer, 
            IConfiguration configuration, 
            IUserService userService)
        {
            this.Admin = admin;
            this.Producer = producer;
            this.Configuration = configuration;
            this.UserService = userService;
        }        

        public void Insert(User userFrom, User userTo, string text, DateTime sentAt)
        {
            var message = new Message<Guid, string>()
            { 
                Value = text,
                Key = userFrom.Guid,
                Timestamp = new Timestamp(sentAt)
            };

            this.Producer.Produce($"msguser{userTo.Guid}", message, this.ProduceHandler);
            this.Producer.Flush(TimeSpan.FromSeconds(10));
        }

        private void ProduceHandler(DeliveryReport<Guid, string> report)
        {
            if (report.Error.IsError)
            {
                if (report.Error.Code == ErrorCode.TopicException && report.Error.Reason.Contains("Invalid topic"))
                {
                    CreateTopic(report.Topic);
                }

                string log = $"Error while inserting message. Error: {report.Error.Reason} - Code: {report.Error.Code}";
                Console.WriteLine(log);
            }
        }

        private void CreateTopic(string topic)
        {
            var topicSpecifications = new TopicSpecification[]
            {
                new TopicSpecification()
                {
                    Name = topic,
                    NumPartitions = 1,
                    ReplicationFactor = 3,
                    Configs = new Dictionary<string, string>() { { "bootstrap.servers", this.Configuration["kafka:server"] } }
                }
            };

            this.Admin.CreateTopicsAsync(topicSpecifications);
        }
    }
}