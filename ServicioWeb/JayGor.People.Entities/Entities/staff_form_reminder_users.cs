using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class staff_form_reminder_users
    {
        public staff_form_reminder_users()
        {
        }

        public long Id { get; set; }
        
        public long IdfStaffFormReminder { get; set; }

        public long IdfUser { get; set; }
    }
}
