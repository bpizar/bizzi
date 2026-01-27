using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormResponse : CommonResponse
    {
        public StaffFormsCustomEntity StaffForm { get; set; } = new StaffFormsCustomEntity();
    }
}
