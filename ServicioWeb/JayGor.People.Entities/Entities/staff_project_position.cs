using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class staff_project_position
    {
        public staff_project_position()
        {
            h_dailylog_involved_people = new HashSet<h_dailylog_involved_people>();
            h_incident_involved_people = new HashSet<h_incident_involved_people>();
            h_medical_reminders = new HashSet<h_medical_reminders>();
            projects_clients = new HashSet<projects_clients>();
            scheduling = new HashSet<scheduling>();
            tasks = new HashSet<tasks>();
            time_tracking = new HashSet<time_tracking>();
            time_tracking_review = new HashSet<time_tracking_review>();
        }

        public long Id { get; set; }
        public long IdfStaff { get; set; }
        public long IdfProject { get; set; }
        public long IdfPosition { get; set; }
        public long IdfPeriod { get; set; }
        public string State { get; set; }
        public long Hours { get; set; }

        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual positions IdfPositionNavigation { get; set; }
        public virtual projects IdfProjectNavigation { get; set; }
        public virtual staff IdfStaffNavigation { get; set; }
        public virtual ICollection<h_dailylog_involved_people> h_dailylog_involved_people { get; set; }
        public virtual ICollection<h_incident_involved_people> h_incident_involved_people { get; set; }
        public virtual ICollection<h_medical_reminders> h_medical_reminders { get; set; }
        public virtual ICollection<projects_clients> projects_clients { get; set; }
        public virtual ICollection<scheduling> scheduling { get; set; }
        public virtual ICollection<tasks> tasks { get; set; }
        public virtual ICollection<time_tracking> time_tracking { get; set; }
        public virtual ICollection<time_tracking_review> time_tracking_review { get; set; }
    }
}
