using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormReminderUserResponse : CommonResponse
    {
        public StaffFormReminderUsersCustomEntity StaffFormReminderUser { get; set; } = new StaffFormReminderUsersCustomEntity();
    }
}
