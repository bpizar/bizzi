using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class staff_period_settings
    {
        public long Id { get; set; }
        public long IdfStaff { get; set; }
        public long IdfPeriod { get; set; }
        public int WorkingHours { get; set; }

        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual staff IdfStaffNavigation { get; set; }
    }
}
