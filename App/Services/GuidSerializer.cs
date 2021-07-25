using System;
using Confluent.Kafka;

namespace App.Services
{
    public class GuidSerializer : ISerializer<Guid>
    {
        public byte[] Serialize(Guid data, SerializationContext context)
            => System.Text.UTF8Encoding.UTF8.GetBytes(data.ToString());
    }
}