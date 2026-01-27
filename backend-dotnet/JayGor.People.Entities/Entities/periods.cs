using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class periods
    {
        public periods()
        {
            h_dailylogs = new HashSet<h_dailylogs>();
            h_incidents = new HashSet<h_incidents>();
            h_injuries = new HashSet<h_injuries>();
            project_owners = new HashSet<project_owners>();
            project_petty_cash = new HashSet<project_petty_cash>();
            projects_clients = new HashSet<projects_clients>();
            scheduling = new HashSet<scheduling>();
            staff_period_settings = new HashSet<staff_period_settings>();
            staff_project_position = new HashSet<staff_project_position>();
            tasks = new HashSet<tasks>();
            time_tracking_review = new HashSet<time_tracking_review>();
        }

        public long Id { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public long? IdfCreatedBy { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<h_dailylogs> h_dailylogs { get; set; }
        public virtual ICollection<h_incidents> h_incidents { get; set; }
        public virtual ICollection<h_injuries> h_injuries { get; set; }
        public virtual ICollection<project_owners> project_owners { get; set; }
        public virtual ICollection<project_petty_cash> project_petty_cash { get; set; }
        public virtual ICollection<projects_clients> projects_clients { get; set; }
        public virtual ICollection<scheduling> scheduling { get; set; }
        public virtual ICollection<staff_period_settings> staff_period_settings { get; set; }
        public virtual ICollection<staff_project_position> staff_project_position { get; set; }
        public virtual ICollection<tasks> tasks { get; set; }
        public virtual ICollection<time_tracking_review> time_tracking_review { get; set; }
    }
}
