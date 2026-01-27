using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllStaffResponse : CommonResponse
    {
        public IEnumerable<StaffCustomEntity> Staffs { get; set; }
        public IEnumerable<positions> Positions { get; set; }
    }
}
