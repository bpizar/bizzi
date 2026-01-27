using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetStatusesResponse: CommonResponse
    {
        public List<statuses> StatusesList { get; set; } = new List<statuses>();
    }
}
