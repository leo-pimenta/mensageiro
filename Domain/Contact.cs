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
        
        public bool IsBlocked 
        { 
            get
            {
                return this.Block != null;
            }
        }

        
    }
}