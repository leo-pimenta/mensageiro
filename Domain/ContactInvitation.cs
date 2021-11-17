using System;

namespace Domain
{
    public class ContactInvitation
    {
        public Guid Id { get; }
        
        public Guid UserId { get; }
        public User User { get; }
        
        public Guid InvitedUserId { get; }
        public User InvitedUser { get; }

        public ContactInvitation(Guid id, User user, User invitedUser)
        {
            this.Id = id;
            this.User = user;
            this.UserId = user.Id;
            this.InvitedUser = invitedUser;
            this.InvitedUserId = invitedUser.Id;
        }

        private ContactInvitation() {}
    }
}