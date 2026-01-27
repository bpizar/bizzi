using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffForPlanningResponse : CommonResponse
    {
        public IEnumerable<StaffForPlanningCustomEntity> Staffs { get; set; } = new List<StaffForPlanningCustomEntity>();
    }
}
