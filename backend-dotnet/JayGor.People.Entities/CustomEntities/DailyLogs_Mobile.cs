using System;

namespace JayGor.People.Entities.CustomEntities
{
    public class DailyLogs_Mobile
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public long ClientId { get; set; }
        public DateTime Date { get; set; }
        public long UserId { get; set; }
		public string Placement { get; set; }
		public string StaffOnShift { get; set; }
		public string GeneralMood { get; set; }
		public string InteractionStaff { get; set; }
		public string InteractionPeers { get; set; }
		public string School { get; set; }
		public string Attended { get; set; }
		public string InHouseProg { get; set; }
		public string Comments { get; set; }
		public string Health { get; set; }
		public string ContactFamily { get; set; }
		public string SeriousOccurrence { get; set; }
		public string Other { get; set; }
		public string clientName { get; set; }
		public string ProjectName { get; set; }
		public string Color { get; set; }
    }
}