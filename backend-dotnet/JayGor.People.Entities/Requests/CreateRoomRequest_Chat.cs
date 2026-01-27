using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Requests
{
    // public class CreateRoomRequest_Chat : CommonRequest
    public class CreateRoomRequest_Chat
    {
        public string RoomName { get; set; }
        public List<ParticipantUserChat> Participants { get; set; }
    }
}