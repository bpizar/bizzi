using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class tasks
    {
        public tasks()
        {
            tasks_reminders = new HashSet<tasks_reminders>();
            tasks_state_history = new HashSet<tasks_state_history>();
        }

        public long Id { get; set; }
        public string Subject { get; set; }
        public long? IdfStatus { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public long? IdfAssignedTo { get; set; }
        public string RecurrencePattern { get; set; }
        public string RecurrenceException { get; set; }
        public int? AllDay { get; set; }
        public long? IdfCreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Address { get; set; }
        public long? IdDuplicate { get; set; }
        public long Hours { get; set; }
        public long? IdfPeriod { get; set; }
        public long? IdfProject { get; set; }
        public long? IdfAssignableRol { get; set; }
        public string Type { get; set; }
        public long? IdfTaskParent { get; set; }
        public string Notes { get; set; }

        public virtual positions IdfAssignableRolNavigation { get; set; }
        public virtual staff_project_position IdfAssignedToNavigation { get; set; }
        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual projects IdfProjectNavigation { get; set; }
        public virtual statuses IdfStatusNavigation { get; set; }
        public virtual ICollection<tasks_reminders> tasks_reminders { get; set; }
        public virtual ICollection<tasks_state_history> tasks_state_history { get; set; }
    }
}
