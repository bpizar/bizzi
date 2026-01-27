using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class client_form_reminder_users
    {
        public client_form_reminder_users()
        {
        }

        public long Id { get; set; }
        
        public long IdfClientFormReminder { get; set; }

        public long IdfUser { get; set; }
    }
}
