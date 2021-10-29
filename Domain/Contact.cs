using System;

namespace Domain
{
    public class Contact
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public Guid ContactUserId { get; set; }
        public User ContactUser { get; set; }
        
        public Guid? BlockId { get; set; }
        public BlockInfo Block { get; set; }
        
        public bool IsBlocked => this.Block != null;

        public Contact(Guid guid, User user, User contactUser, BlockInfo blockInfo = null)
        {
            this.Id = guid;
            this.User = user;
            this.UserId = user.Id;
            this.ContactUser = contactUser;
            this.ContactUserId = contactUser.Id;
            this.Block = blockInfo;
            this.BlockId = blockInfo?.Id;
        }

        private Contact() {}
    }
}