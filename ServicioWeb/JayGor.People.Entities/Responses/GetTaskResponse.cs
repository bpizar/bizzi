using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetTaskResponse : CommonResponse
    {
        public List<TasksForPlanningCustomEntity> Tasks { get; set; } = new List<TasksForPlanningCustomEntity>();

        public List<TasksForPlanningCustomEntity> UnAssignedTasks { get; set; } = new List<TasksForPlanningCustomEntity>();

        public List<duplicate_tasks> Duplicates { get; set; } = new List<duplicate_tasks>();

    }
}
