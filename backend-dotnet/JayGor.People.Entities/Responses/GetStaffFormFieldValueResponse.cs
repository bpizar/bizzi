using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormFieldValueResponse : CommonResponse
    {
        public StaffFormFieldValuesCustomEntity StaffFormFieldValue { get; set; } = new StaffFormFieldValuesCustomEntity();
    }
}
