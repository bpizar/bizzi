using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class tasks_reminders
    {
        public long Id { get; set; }
        public long IdfTask { get; set; }
        public long IdfSettingReminderTime { get; set; }
        public string State { get; set; }
        public long IdfPeriod { get; set; }

        public virtual settings_reminder_time IdfSettingReminderTimeNavigation { get; set; }
        public virtual tasks IdfTaskNavigation { get; set; }
    }
}
