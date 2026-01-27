using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveProjectFormReminderRequest : CommonRequest
    {
        public project_form_reminders ProjectFormReminder { get; set; } = new project_form_reminders();
    }
}
