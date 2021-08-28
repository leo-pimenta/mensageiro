using System;
using System.ComponentModel.DataAnnotations;

namespace App.Dtos
{
    public class CreateContactInvitationDto
    {
        [Required]
        [EmailAddress]
        public string ContactUserEmail { get; set; }
    }
}