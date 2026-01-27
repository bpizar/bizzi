using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormFieldResponse : CommonResponse
    {
        public StaffFormFieldsCustomEntity StaffFormField { get; set; } = new StaffFormFieldsCustomEntity();
    }
}
