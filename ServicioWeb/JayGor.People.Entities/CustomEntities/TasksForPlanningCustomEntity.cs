using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.CustomEntities
{
    public class TasksForPlanningCustomEntity : tasks
    {
        public string AssignedToColor { get; set; }
        public string AssignedToFullName { get; set; }
        public string Status { get; set; }
        public long IdUser { get; set; }
        public string AssignedToPosition { get; set; }
        public string ProjectName { get; set; }
        public string ProjectColor { get; set; }
        public long IdfStaff { get; set; }
        public string Abm { get; set; }
        public string Group { get; set; }
        public bool Draggable { get; set; } = false;
        public bool Resizable { get; set; } = false;
        public string Img { get; set; } 


    }
}
