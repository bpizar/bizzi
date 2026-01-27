using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllStaffFormValuesResponse : CommonResponse
    {
        public IEnumerable<StaffFormValuesCustomEntity> StaffFormValues { get; set; }
    }
}
