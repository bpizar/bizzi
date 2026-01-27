using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormImageValueResponse : CommonResponse
    {
        public StaffFormImageValuesCustomEntity StaffFormImageValue { get; set; } = new StaffFormImageValuesCustomEntity();
    }
}
