using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetPositionsResponse : CommonResponse
    {
        public List<positions> Positions { get; set; } = new List<positions>();
    }
}
