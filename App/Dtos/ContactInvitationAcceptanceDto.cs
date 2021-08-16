using System;
using System.ComponentModel.DataAnnotations;

namespace App.Dtos
{
    public class ContactInvitationAcceptanceDto
    {
        [Required]
        public Guid InvitationGuid { get; set; }
    }
}