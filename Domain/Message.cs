using System;

namespace Domain
{
    public struct Message
    {
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
    }
}