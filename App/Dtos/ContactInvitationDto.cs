using System;
using System.Text.Json.Serialization;

namespace App.Dtos
{
    public class ContactInvitationDto
    {
        [JsonPropertyName("guid")]
        public Guid Guid { get; internal set; }

        [JsonPropertyName("userGuid")]
        public Guid UserGuid { get; internal set; }

        [JsonPropertyName("invitedUserGuid")]
        public Guid InvitedUserGuid { get; internal set; }
        
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }
    }
}