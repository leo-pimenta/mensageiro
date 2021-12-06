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

        public Message() {}

        public Message(ChatGroup group, User user, string text, DateTime sentAt)
        {
            this.Group = group;
            this.GroupId = this.Group.Id;
            this.User = user;
            this.UserId = this.User.Id;
            this.SentAt = sentAt;
            this.Text = text;
        }

        public Message(Guid groupId, Guid userId, string text, DateTime sentAt)
        {
            this.GroupId = groupId;
            this.UserId = userId;
            this.Text = text;
            this.SentAt = sentAt;
        }
    }
}