using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_medical_reminders
    {
        public long Id { get; set; }
        public long IdfClient { get; set; }
        public long IdfAssignedTo { get; set; }
        public string Description { get; set; }
        public DateTime Datetime { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public sbyte Reminder { get; set; }
        public string State { get; set; }

        public virtual staff_project_position IdfAssignedToNavigation { get; set; }
        public virtual clients IdfClientNavigation { get; set; }
    }
}
