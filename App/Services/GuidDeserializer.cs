using System;
using Confluent.Kafka;

namespace App.Services
{
    public class GuidDeserializer : IDeserializer<Guid>
    {
        public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
            => isNull ? default(Guid) : Guid.Parse(System.Text.UTF8Encoding.UTF8.GetString(data));
    }
}