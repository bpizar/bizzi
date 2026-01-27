using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveStaffFormReminderUserRequest : CommonRequest
    {
        public staff_form_reminder_users StaffFormReminderUser { get; set; } = new staff_form_reminder_users();
    }
}
