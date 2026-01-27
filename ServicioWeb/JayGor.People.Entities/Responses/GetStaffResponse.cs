using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffResponse : CommonResponse
    {

        public List<Identity_RolesCustom> Roles { get; set; } = new List<Identity_RolesCustom>();
        public StaffCustomEntity Staff { get; set; } = new StaffCustomEntity();
    }
}
