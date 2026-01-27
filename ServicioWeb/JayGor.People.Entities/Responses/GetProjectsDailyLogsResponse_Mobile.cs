using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectsDailyLogsResponse_Mobile :CommonResponse
    {
		public List<ProjectsDailyLogs_Mobile> Projects { get; set; } = new List<ProjectsDailyLogs_Mobile>();

	}
}
