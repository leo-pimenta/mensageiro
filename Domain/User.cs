using System;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }

        public User() {}
        
        public User(Guid guid, string email, string nickname)
        {
            this.Id = guid;
            this.Email = email;
            this.Nickname = nickname;
        }
    }
}