using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffsResponse : CommonResponse
    {
        public IEnumerable<StaffCustomEntity> Staffs { get; set; } = new List<StaffCustomEntity>();



        
    }
}