using System;

namespace Domain
{
    public class ContactInvitation
    {
        public Guid Guid { get; set; }
        
        public Guid UserGuid { get; set; }
        public User User { get; set; }
        
        public Guid InvitedUserGuid { get; set; }
        public User InvitedUser { get; set; }
    }
}