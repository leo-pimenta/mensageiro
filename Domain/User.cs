using System;

namespace Domain
{
    public class User
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }

        public User(Guid guid, string email, string nickname)
        {
            this.Guid = guid;
            this.Email = email;
            this.Nickname = nickname;
        }
    }
}