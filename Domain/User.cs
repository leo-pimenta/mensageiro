using System;

namespace Domain
{
    public class User
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
    }
}