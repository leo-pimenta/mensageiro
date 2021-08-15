using System;

namespace App.Dtos
{
    public class ContactInvitationDto
    {
        public Guid UserGuid { get; set; }
        public string ContactUserEmail { get; set; }
    }
}