using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class time_tracking_review
    {
        public long Id { get; set; }
        public long IdfStaffProjectPosition { get; set; }
        public long IdfPeriod { get; set; }
        public long SecondsScheduledTime { get; set; }
        public long SecondsUserTracking { get; set; }
        public long SecondsModifiedTracking { get; set; }
        public string State { get; set; }

        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual staff_project_position IdfStaffProjectPositionNavigation { get; set; }
    }
}
