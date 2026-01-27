using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetSchedulingResponse : CommonResponse
    {
        public List<SchedulingCustomEntity> Scheduling { get; set; } = new List<SchedulingCustomEntity>();

		public List<StaffForPlanningCustomEntity> Staffs { get; set; } = new List<StaffForPlanningCustomEntity>();
        public List<OverTimeCustomEntity> OverTime { get; set; } = new List<OverTimeCustomEntity>();
        public List<OverLapCustomEntity> OverLap { get; set; } = new List<OverLapCustomEntity>();


        public List<ProjectCustomEntity> Projects { get; set; } = new List<ProjectCustomEntity>();

    }
}