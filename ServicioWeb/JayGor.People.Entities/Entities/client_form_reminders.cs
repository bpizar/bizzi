using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JayGor.People.Entities.Entities
{
    public partial class client_form_reminders
    {
        public client_form_reminders()
        {
        }

        public long Id { get; set; }
        
        public long IdfClientForm { get; set; }

        public long IdfReminderLevel { get; set; }

        public long IdfPeriodType { get; set; }

        public long IdfPeriodValue { get; set; }

        [NotMapped]
        public long[] IdfUsers { get; set; }
    }
}
