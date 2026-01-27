using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetProject2Response : CommonResponse
    {        
        public List<TasksForPlanningCustomEntity> Tasks { get; set; } = new List<TasksForPlanningCustomEntity>();
        public List<StaffCustomEntity> Staffs { get; set; } = new List<StaffCustomEntity>();
        public decimal TasksHours { get; set; } = 0;
        public List<tasks_reminders> TasksReminders { get; set; } = new List<tasks_reminders>();

        public List<ClientCustomEntity> Clients { get; set; } = new List<ClientCustomEntity>();

		public List<ProjectOwnersCustom> Owners { get; set; } = new List<ProjectOwnersCustom>();
		public List<StaffCustomEntity> StaffsForOwners { get; set; } = new List<StaffCustomEntity>();

        public bool ForceLoad { get; set; }
    }
}