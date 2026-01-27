using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class SaveIncidentResponse : CommonResponse
    {
        public List<h_injuries> Injuries { get; set; } = new List<h_injuries>();
    }
}
