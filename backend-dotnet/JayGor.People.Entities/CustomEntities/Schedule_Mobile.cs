using System;
namespace JayGor.People.Entities.CustomEntities
{
	public class ScheduleMobile
	{
        public long Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string ProjectName { get; set; }
        public string Color { get; set; }
	}
}