using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveClientFormReminderRequest : CommonRequest
    {
        public client_form_reminders ClientFormReminder { get; set; } = new client_form_reminders();
    }
}
