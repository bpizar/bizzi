using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.Responses
{
    public class GetTasksByStaffResponse : CommonResponse
    {
        public decimal AvailableHoursOnPeriod { get; set; }
        public decimal AssignedHoursOnPeriod { get; set; }                
        public List<TasksForPlanningCustomEntity> AssignedTasks {get;set;}=new List<TasksForPlanningCustomEntity>();

        public string AssignedPrograms { get; set; }

        public staff_period_settings StaffPeriodSettings { get; set; } = new staff_period_settings();

	
    }
}
