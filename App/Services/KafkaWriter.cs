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
        private readonly TimeSpan FlushTimeout;

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
            this.FlushTimeout = TimeSpan.FromSeconds(10);
        }        

        public void Insert(User userFrom, User userTo, string text, DateTime sentAt)
        {
            Message<Guid, string> message = this.CreateMessage(text, userFrom.Id, sentAt);
            this.Produce($"msguser{userTo.Id}", message);
            //Flush();
        }

        public void InsertContactInvitation(ContactInvitation invitation, DateTime sentAt)
        {
            string text = $"{invitation.User.Nickname}/{invitation.User.Email} has requests to add you as a contact.";
            Message<Guid, string> message = this.CreateMessage(text, invitation.UserId, sentAt);
            this.Produce($"msgcontactrequest{invitation.InvitedUserId}", message);
            //this.Flush();
        }

        private void Produce(string topic, Message<Guid, string> message)
        {
            this.Producer.Produce(topic, message, this.ProduceHandler);
        }

        // TODO must flush before closing application...
        private void Flush()
        {
            this.Producer.Flush(this.FlushTimeout);
        }

        private Message<Guid, string> CreateMessage(string text, Guid identifier, DateTime sentAt)
            => new Message<Guid, string>()
            { 
                Value = text,
                Key = identifier,
                Timestamp = new Timestamp(sentAt)
            };

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