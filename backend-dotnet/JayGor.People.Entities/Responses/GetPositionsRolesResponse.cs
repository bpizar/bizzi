using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetPositionsRolesResponse : CommonResponse
    {
        //public List<PositionsRolesCustom> PositionsRolesList { get; set; } = new List<PositionsRolesCustom>();
        public List<identity_roles> Roles { get; set; } = new List<identity_roles>();
        public List<positions> Positions { get; set; } = new List<positions>();
    }
}
