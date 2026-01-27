using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class time_tracking
    {
        public long Id { get; set; }
        public long IdfStaffProjectPosition { get; set; }
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
        public int status { get; set; }
        public string startNote { get; set; }
        public string endNote { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public float? endLong { get; set; }
        public float? endLat { get; set; }

        public virtual staff_project_position IdfStaffProjectPositionNavigation { get; set; }
    }
}
