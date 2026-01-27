using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SavePettyCashRequest : CommonRequest
    {
        public List<ProjectPettyCashCustom> PettyCash { get; set; } = new List<ProjectPettyCashCustom>();
        public long IdPeriod { get; set; }
    }
}