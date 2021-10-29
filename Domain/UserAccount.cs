using System;

namespace Domain
{
    public class UserAccount
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string HashedPassword { get; set; }

        public UserAccount(User user, string hashedPassword)
        {
            this.User = user;
            this.UserId = user.Id;
            this.HashedPassword = hashedPassword;
        }

        private UserAccount() {}
    }
}