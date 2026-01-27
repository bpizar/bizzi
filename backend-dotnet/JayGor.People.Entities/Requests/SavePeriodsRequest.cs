using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SavePeriodsRequest : CommonRequest
    {
        public List<PeriodsCustom> Periods { get; set; }
    }
}