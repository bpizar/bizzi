using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class project_form_reminder_users
    {
        public project_form_reminder_users()
        {
        }

        public long Id { get; set; }
        
        public long IdfProjectFormReminder { get; set; }

        public long IdfUser { get; set; }
    }
}
