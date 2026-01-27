using System;

namespace JayGor.People.Entities.CustomEntities
{
    public class MessageChat
    {
		public long Id;
		public string Msg;
		public string State;
        public string SenderName;
        public long SenderId;
        public long IdRoom;
        public bool IsYou;
        public string DateSent;
    }
}


