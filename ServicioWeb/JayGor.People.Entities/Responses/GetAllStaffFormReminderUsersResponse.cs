using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllStaffFormReminderUsersResponse : CommonResponse
    {
        public IEnumerable<StaffFormReminderUsersCustomEntity> StaffFormReminderUsers { get; set; }
    }
}
