using System;
using System.Text.Json.Serialization;

namespace App.Dtos
{
    public class UserDto
    {
        [JsonPropertyName("guid")]
        public Guid Guid { get; set; }
        
        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }
        
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}