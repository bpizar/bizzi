using System;
namespace JayGor.People.Entities.CustomEntities
{
    public class ReminderForPanel
    {
        public long IdTask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeadLine { get; set; }
        public string Color { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string Img { get; set;  }
    }
}