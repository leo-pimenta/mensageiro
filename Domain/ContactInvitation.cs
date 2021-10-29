using System;

namespace Domain
{
    public class ContactInvitation
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public Guid InvitedUserId { get; set; }
        public User InvitedUser { get; set; }
    }
}