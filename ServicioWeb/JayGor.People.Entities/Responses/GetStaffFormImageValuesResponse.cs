using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormImageValuesResponse : CommonResponse
    {
        public IEnumerable<StaffFormImageValuesCustomEntity> StaffFormImageValues { get; set; } = new List<StaffFormImageValuesCustomEntity>();
    }
}