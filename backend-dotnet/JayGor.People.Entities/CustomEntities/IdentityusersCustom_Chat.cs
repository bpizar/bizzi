using System;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.CustomEntities
{
    public class IdentityusersCustom_Chat : identity_users
    {

        public long ServerRoomVersion { get; set; } 
		public long ServerMessagesVersion { get; set; }
		public long ServerParticipantsVersion { get; set; }

    }
}


