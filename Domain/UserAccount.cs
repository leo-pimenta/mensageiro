using System;

namespace Domain
{
    public class UserAccount
    {
        public Guid UserGuid { get; set; }
        public User User { get; set; }
        public string HashedPassword { get; set; }

        public UserAccount(User user, string hashedPassword)
        {
            this.User = user;
            this.UserGuid = user.Guid;
            this.HashedPassword = hashedPassword;
        }

        private UserAccount() {}
    }
}