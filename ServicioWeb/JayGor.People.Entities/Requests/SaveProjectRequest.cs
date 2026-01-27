using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveProjectRequest : CommonRequest
    {
        public ProjectCustomEntity Project{ get; set; } = new ProjectCustomEntity();
        public List<TasksForPlanningCustomEntity> Tasks { get; set; } = new List<TasksForPlanningCustomEntity>();
        public List<Staff_Project_PositionCustomEntity> Staffs { get; set; } = new List<Staff_Project_PositionCustomEntity>();
        public List<ProjectOwnersCustom> Owners { get; set; } = new List<ProjectOwnersCustom>();
        public long IdPeriod { get; set; }
        public List<tasks_reminders> tasksReminders { get; set; } = new List<tasks_reminders>();

        public List<ClientCustomEntity> clients { get; set; } = new List<ClientCustomEntity>(); 

        public bool ForceGetData { get; set; }
    }
}