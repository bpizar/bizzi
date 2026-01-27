using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveClientFormReminderUserRequest : CommonRequest
    {
        public client_form_reminder_users ClientFormReminderUser { get; set; } = new client_form_reminder_users();
    }
}
