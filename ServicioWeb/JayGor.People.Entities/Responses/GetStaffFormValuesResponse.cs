using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormValuesResponse : CommonResponse
    {
        public IEnumerable<StaffFormValuesCustomEntity> StaffFormValues { get; set; } = new List<StaffFormValuesCustomEntity>();
    }
}