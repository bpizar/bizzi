using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public IEnumerable<tasks> GetReport1(List<long> projectIds, DateTime from, DateTime to)
        {
            return dataAccessLayer.GetReport1(projectIds, from, to);
        }

        public IEnumerable<ReportProjectsDetailsCustomEntity> GetReportProjects()
        {
            return dataAccessLayer.GetReportProjects();
        }

		public List<TaskHistoryReportCustomEntity> GetTaskHistoryReport(long idProject, long idPeriod)
		{
			return dataAccessLayer.GetTaskHistoryReport(idProject, idPeriod);
		}
    }
}
