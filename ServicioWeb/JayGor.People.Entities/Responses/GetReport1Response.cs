using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetReport1Response : CommonResponse
    {
        public List<tasks> Tasks { get; set; } = new List<tasks>();
    }
}
