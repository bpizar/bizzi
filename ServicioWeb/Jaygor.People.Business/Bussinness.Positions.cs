using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public IEnumerable<positions> GetPositions()
        {
            return dataAccessLayer.GetPositions();
        }

        //public IEnumerable<PositionsRolesCustom> GetPositionsRoles()
        //{
        //    return dataAccessLayer.GetPositionsRoles();
        //}

        public CommonResponse SavePositions(List<PositionsCustom> Positions)
        {
            return dataAccessLayer.SavePositions(Positions);
        }
    }
}