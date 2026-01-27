using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class settings_reminder_time
    {
        public settings_reminder_time()
        {
            tasks_reminders = new HashSet<tasks_reminders>();
        }

        public long Id { get; set; }
        public long MinutesBefore { get; set; }
        public string State { get; set; }

        public virtual ICollection<tasks_reminders> tasks_reminders { get; set; }
    }
}
