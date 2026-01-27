using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormFieldsResponse : CommonResponse
    {
        public IEnumerable<StaffFormFieldsCustomEntity> StaffFormFields { get; set; } = new List<StaffFormFieldsCustomEntity>();
        
    }
}