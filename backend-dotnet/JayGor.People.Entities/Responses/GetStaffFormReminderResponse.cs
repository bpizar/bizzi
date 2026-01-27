using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormReminderResponse : CommonResponse
    {
        public StaffFormRemindersCustomEntity StaffFormReminder { get; set; } = new StaffFormRemindersCustomEntity();
    }
}
