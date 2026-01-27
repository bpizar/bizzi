using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JayGor.People.Entities.Entities
{
    public partial class project_form_reminders
    {
        public project_form_reminders()
        {
        }

        public long Id { get; set; }
        
        public long IdfProjectForm { get; set; }

        public long IdfReminderLevel { get; set; }

        public long IdfPeriodType { get; set; }

        public long IdfPeriodValue { get; set; }

        [NotMapped]
        public long[] IdfUsers { get; set; }
    }
}
