using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_dailylog_involved_people
    {
        public long Id { get; set; }
        public long IdfDailyLog { get; set; }
        public long IdfSPP { get; set; }
        public string IdentifierGroup { get; set; }
        public string State { get; set; }

        public virtual h_dailylogs IdfDailyLogNavigation { get; set; }
        public virtual staff_project_position IdfSPPNavigation { get; set; }
    }
}
