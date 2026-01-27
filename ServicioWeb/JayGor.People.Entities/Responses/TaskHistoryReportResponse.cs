using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;


namespace JayGor.People.Entities.Responses
{
	public class TaskHistoryReportResponse : CommonResponse
	{
		public List<TaskHistoryReportCustomEntity> tasks { get; set; } = new List<TaskHistoryReportCustomEntity>();
	}
}
