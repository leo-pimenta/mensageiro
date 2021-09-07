using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain;

namespace App.Dtos
{
    public class ChatMessagesResponseDto
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; }

        [JsonPropertyName("groupId")]
        public Guid GroupId { get; }

        [JsonPropertyName("messages")]
        public IEnumerable<Message> Messages { get; }

        [JsonPropertyName("page")]
        public int Page { get; }
        
        public ChatMessagesResponseDto(Guid userId, Guid groupId, IEnumerable<Message> messages, int page)
        {
            UserId = userId;
            GroupId = groupId;
            Messages = messages;
            Page = page;
        }
    }
}