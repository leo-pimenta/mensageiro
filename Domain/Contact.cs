using System;

namespace Domain
{
    public class Contact
    {
        public Guid Guid { get; set; }
        public User User { get; set; }
        public User ContactUser { get; set; }
        public BlockInfo Block { get; set; }
        
        public bool HasBlocked 
        { 
            get
            {
                return this.Block != null;
            }
        }
    }
}