using System;
using System.Text.Json.Serialization;
using Domain;

namespace App.Dtos
{
    public class MessageDto
    {
        [JsonPropertyName("sentAt")]
        public DateTime SentAt { get; set; }
        
        [JsonPropertyName("userNickname")]
        public string UserNickname { get; set; }

        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("groupId")]
        public Guid GroupId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        public MessageDto(Message message)
        {
            this.GroupId = message.GroupId;
            this.SentAt = message.SentAt;
            this.UserId = message.UserId;
            this.UserNickname = message.User.Nickname;
            this.Text = message.Text;
        }
    }
}