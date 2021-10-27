using System;

namespace Domain
{
    public class Contact
    {
        public Guid Guid { get; set; }
        
        public Guid UserGuid { get; set; }
        public User User { get; set; }
        
        public Guid ContactUserGuid { get; set; }
        public User ContactUser { get; set; }
        
        public Guid? BlockGuid { get; set; }
        public BlockInfo Block { get; set; }
        
        public bool IsBlocked => this.Block != null;

        public Contact(Guid guid, User user, User contactUser, BlockInfo blockInfo = null)
        {
            this.Guid = guid;
            this.User = user;
            this.UserGuid = user.Guid;
            this.ContactUser = contactUser;
            this.ContactUserGuid = contactUser.Guid;
            this.Block = blockInfo;
            this.BlockGuid = blockInfo?.Guid;
        }

        private Contact() {}
    }
}