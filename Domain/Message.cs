using System;

namespace Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public ChatGroup Group { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
    }
}