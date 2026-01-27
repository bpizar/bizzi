using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class chat_room_participants
    {
        public chat_room_participants()
        {
            chat_message_participant_state = new HashSet<chat_message_participant_state>();
        }

        public long Id { get; set; }
        public long IdfIdentityUser { get; set; }
        public long IdfChatRoom { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public virtual chat_rooms IdfChatRoomNavigation { get; set; }
        public virtual identity_users IdfIdentityUserNavigation { get; set; }
        public virtual ICollection<chat_message_participant_state> chat_message_participant_state { get; set; }
    }
}
