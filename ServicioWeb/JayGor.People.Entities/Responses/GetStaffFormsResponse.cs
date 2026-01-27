using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffFormsResponse : CommonResponse
    {
        public IEnumerable<StaffFormsCustomEntity> StaffForms { get; set; } = new List<StaffFormsCustomEntity>();
        
    }
}