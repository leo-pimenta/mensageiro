using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain;

namespace App.Dtos
{
    public class ChatMessagesResponseDto
    {
        [JsonPropertyName("groupId")]
        public Guid GroupId { get; }

        [JsonPropertyName("messages")]
        public IEnumerable<MessageDto> Messages { get; }

        [JsonPropertyName("page")]
        public int Page { get; }
        
        public ChatMessagesResponseDto(Guid groupId, IEnumerable<MessageDto> messages, int page)
        {
            GroupId = groupId;
            Messages = messages;
            Page = page;
        }
    }
}