using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Requests
{
    public class SavePositionsRolesRequest
        : CommonRequest
    {
        //public List<PositionsRolesCustom> PositionsRolesList { get; set; } = new List<PositionsRolesCustom>();
        public List<PositionsCustom> Positions { get; set; } = new List<PositionsCustom>();
    }
}
