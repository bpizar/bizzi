using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class chat_rooms
    {
        public chat_rooms()
        {
            chat_messages = new HashSet<chat_messages>();
            chat_room_participants = new HashSet<chat_room_participants>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }

        public virtual ICollection<chat_messages> chat_messages { get; set; }
        public virtual ICollection<chat_room_participants> chat_room_participants { get; set; }
    }
}
