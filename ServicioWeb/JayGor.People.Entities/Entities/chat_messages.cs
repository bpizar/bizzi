using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class chat_messages
    {
        public chat_messages()
        {
            chat_message_participant_state = new HashSet<chat_message_participant_state>();
        }

        public long Id { get; set; }
        public string Messages { get; set; }
        public DateTime Date { get; set; }
        public long IdfIdentityUserSender { get; set; }
        public long IdfChatRoom { get; set; }

        public virtual chat_rooms IdfChatRoomNavigation { get; set; }
        public virtual identity_users IdfIdentityUserSenderNavigation { get; set; }
        public virtual ICollection<chat_message_participant_state> chat_message_participant_state { get; set; }
    }
}
