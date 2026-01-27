using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class chat_message_participant_state
    {
        public long Id { get; set; }
        public long IdfMessage { get; set; }
        public long IdfParticipant { get; set; }
        public string Delivered { get; set; }
        public string Read { get; set; }

        public virtual chat_messages IdfMessageNavigation { get; set; }
        public virtual chat_room_participants IdfParticipantNavigation { get; set; }
    }
}
