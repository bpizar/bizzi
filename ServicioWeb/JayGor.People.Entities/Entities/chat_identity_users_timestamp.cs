using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class chat_identity_users_timestamp
    {
        public long Id { get; set; }
        public long IdfIdentityUser { get; set; }
        public long ClientRoomVersion { get; set; }
        public long ClientParticipantsVersion { get; set; }
        public long ClientMessagesVersion { get; set; }
        public long ServerRoomVersion { get; set; }
        public long ServerParticipantsVersion { get; set; }
        public long ServerMessagesVersion { get; set; }
        public DateTime DatePushSent { get; set; }

        public virtual identity_users IdfIdentityUserNavigation { get; set; }
    }
}
