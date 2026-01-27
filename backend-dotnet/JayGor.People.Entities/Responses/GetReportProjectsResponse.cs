using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetReportProjectsResponse : CommonResponse
    {
        public List<ReportProjectsDetailsCustomEntity> Details { get; set; } = new List<ReportProjectsDetailsCustomEntity>();
    }
}
