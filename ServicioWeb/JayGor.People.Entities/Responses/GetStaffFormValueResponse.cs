using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormValueResponse : CommonResponse
    {
        public StaffFormValuesCustomEntity StaffFormValue { get; set; } = new StaffFormValuesCustomEntity();
    }
}
