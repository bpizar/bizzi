using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Requests
{
    public class UpdateMessagesStateRequest_Chat
    {
		public List<long> DeliveredMessagesIds { get; set; }
        public List<long> ReadMessagesIds { get; set; }
        public long UserId { get; set; }
	}
}
