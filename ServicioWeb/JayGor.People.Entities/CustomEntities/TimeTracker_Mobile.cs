using System;

namespace JayGor.People.Entities.CustomEntities
{
	public class TimeTracker_Mobile
	{
		public long id { get; set; }
        public DateTime start { get; set; }
		public DateTime?end { get; set; }
		public string Color { get; set; }
		public string ProjectName { get; set; }
	}
}