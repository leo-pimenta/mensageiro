using System;
using System.ComponentModel.DataAnnotations;

namespace App.Dtos
{
    public class ContactInvitationDto
    {
        [Required]
        public Guid UserGuid { get; set; }

        [Required]
        [EmailAddress]
        public string ContactUserEmail { get; set; }
    }
}