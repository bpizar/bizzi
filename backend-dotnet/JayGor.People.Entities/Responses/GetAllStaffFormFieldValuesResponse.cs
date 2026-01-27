using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllStaffFormFieldValuesResponse : CommonResponse
    {
        public IEnumerable<StaffFormFieldValuesCustomEntity> StaffFormFieldValues { get; set; }
    }
}
