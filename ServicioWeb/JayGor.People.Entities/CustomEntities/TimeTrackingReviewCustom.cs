using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class TimeTrackingReviewCustom : time_tracking_review
    {
        public string PositionName { get; set; }
        public string ProjectName { get; set; }
        public string ParticipantFullName { get; set; }
        public string ScheduledTimeFormat { get; set; }
        public string UserTrackingFormat { get; set; }
        public string ModifiedTrackingFormat { get; set; }
        public string Abm { get; set; }      


        public string Img { get; set; }
        public string ProjectColor { get; set; }

        public long IdfProject { get; set; }
    }
}
