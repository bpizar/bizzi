using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffAndPositionsForCopyWindowResponse : CommonResponse
    {        
        public List<StaffCustomEntity> Staffs { get; set; } = new List<StaffCustomEntity>();
        public List<positions> Positions { get; set; } = new List<positions>();
    }
}