using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetPeriodsResponse: CommonResponse
    {
        public List<PeriodsCustom> PeriodsList { get; set; } = new List<PeriodsCustom>();
    }
}
