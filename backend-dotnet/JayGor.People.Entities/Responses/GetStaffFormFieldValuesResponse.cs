using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormFieldValuesResponse : CommonResponse
    {
        public IEnumerable<StaffFormFieldValuesCustomEntity> StaffFormFieldValues { get; set; } = new List<StaffFormFieldValuesCustomEntity>();
    }
}