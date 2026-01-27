using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
	public class GetDailyLogsByUserIdResponse_Mobile : CommonResponse
	{
        public List<DailyLogs_Mobile> DailyLogs { get; set; }
	}
}


