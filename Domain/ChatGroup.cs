using System;

namespace Domain
{
    public class ChatGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ChatGroup(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public ChatGroup(Guid id)
        {
            this.Id = id;
        }
    }
}