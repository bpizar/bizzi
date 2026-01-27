using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class projects
    {
        public projects()
        {
            h_dailylogs = new HashSet<h_dailylogs>();
            h_injuries = new HashSet<h_injuries>();
            project_owners = new HashSet<project_owners>();
            project_petty_cash = new HashSet<project_petty_cash>();
            projects_clients = new HashSet<projects_clients>();
            scheduling = new HashSet<scheduling>();
            staff_project_position = new HashSet<staff_project_position>();
            tasks = new HashSet<tasks>();
        }

        public long Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Color { get; set; }
        public string State { get; set; }
        public int Visible { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public virtual ICollection<h_dailylogs> h_dailylogs { get; set; }
        public virtual ICollection<h_injuries> h_injuries { get; set; }
        public virtual ICollection<project_owners> project_owners { get; set; }
        public virtual ICollection<project_petty_cash> project_petty_cash { get; set; }
        public virtual ICollection<projects_clients> projects_clients { get; set; }
        public virtual ICollection<scheduling> scheduling { get; set; }
        public virtual ICollection<staff_project_position> staff_project_position { get; set; }
        public virtual ICollection<tasks> tasks { get; set; }
    }
}
