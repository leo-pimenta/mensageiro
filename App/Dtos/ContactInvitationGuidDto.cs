using System;
using System.ComponentModel.DataAnnotations;

namespace App.Dtos
{
    public class ContactInvitationGuidDto
    {
        [Required]
        public Guid InvitationGuid { get; set; }
    }
}