using System;

namespace Domain
{
    public class UserGroupRelationship
    {
        public Guid GroupId { get; set; }
        public ChatGroup Group { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public UserGroupRelationship(User user, ChatGroup group)
        {
            this.User = user;
            this.UserId = user.Id;
            this.Group = group;
            this.GroupId = group.Id;
        }

        private UserGroupRelationship() {}
    }
}