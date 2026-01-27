using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_dailylogs
    {
        public h_dailylogs()
        {
            h_dailylog_involved_people = new HashSet<h_dailylog_involved_people>();
        }

        public long Id { get; set; }
        public long IdfPeriod { get; set; }
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
        public string State { get; set; }

        public virtual clients Client { get; set; }
        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual projects Project { get; set; }
        public virtual ICollection<h_dailylog_involved_people> h_dailylog_involved_people { get; set; }
    }
}
