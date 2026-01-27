using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveProjectFormReminderUserRequest : CommonRequest
    {
        public project_form_reminder_users ProjectFormReminderUser { get; set; } = new project_form_reminder_users();
    }
}
