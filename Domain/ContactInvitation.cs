using System;

namespace Domain
{
    public class ContactInvitation
    {
        public Guid Guid { get; set; }
        public User User { get; set; }
        public User InvitedUser { get; set; }
    }
}