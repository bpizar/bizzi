using System;

namespace JayGor.People.Entities.CustomEntities
{
	public class MyTasks_Mobile
	{
		public long id { get; set; }
        public string AssignationType { get; set; }
		public string Subject { get; set; }
		public string idfStatus { get; set; }
        public DateTime Deadline { get; set; }
		public string position { get; set; }
		public string ProjectName { get; set; }
		public string Color { get; set; }
        public string type { get; set; }
	}
}