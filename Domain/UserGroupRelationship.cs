using System;

namespace Domain
{
    public class UserGroupRelationship
    {
        public Guid GroupId { get; set; }
        public ChatGroup Group { get; set; }
        public Guid UserId { get; set; }
        public User user { get; set; }
    }
}