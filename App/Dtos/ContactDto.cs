using System;
using System.Text.Json.Serialization;

namespace App.Dtos
{
    public class ContactDto
    {
        [JsonPropertyName("guid")]
        public Guid Guid { get; set; }

        [JsonPropertyName("contact")]
        public UserDto Contact { get; set; }

        [JsonPropertyName("isBlocked")]
        public bool IsBlocked { get; set; }
        
        [JsonPropertyName("groupId")]
        public Guid GroupId { get; set; }
    }
}