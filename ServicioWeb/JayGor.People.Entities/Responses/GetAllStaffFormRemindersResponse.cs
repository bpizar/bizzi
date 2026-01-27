using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllStaffFormRemindersResponse : CommonResponse
    {
        public IEnumerable<StaffFormRemindersCustomEntity> StaffFormReminders { get; set; }
    }
}
