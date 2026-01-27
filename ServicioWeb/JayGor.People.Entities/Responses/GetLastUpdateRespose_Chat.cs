using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetLastUpdateRespose_Chat : CommonResponse
    {
        public long IdUser { get; set; }
        public long RoomVersion { get; set; }
        public long ParticipantsVersion { get; set; }
        public long MessagesVersion { get; set; }

        public List<RoomChat> Rooms { get; set; }
        public List<ParticipantUserChat> GlobalParticipants { get; set; }
    }
}