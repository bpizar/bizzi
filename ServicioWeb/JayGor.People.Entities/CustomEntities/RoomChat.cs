using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.CustomEntities
{
    public class RoomChat
    {
		public long Id;
		public string Name;
		// public int UnReadMessages;
		public List<ParticipantRoomChat> Participants;
		public List<MessageChat> Messages;
		public DateTime LastMessage;
        public string ImgRoom;
    }
}


