using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveStaffFormReminderRequest : CommonRequest
    {
        public staff_form_reminders StaffFormReminder { get; set; } = new staff_form_reminders();
    }
}
