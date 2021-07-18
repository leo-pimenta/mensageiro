using System;

namespace Domain
{
    public class UserAccount
    {
        public Guid UserGuid { get; set; }
        public User User { get; set; }
        public string HashedPassword { get; set; }
    }
}