using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class getSchedulebyIdResponse_Mobile : CommonResponse
    {
        public List<ScheduleMobile> Schedule { get; set; }
    }
}
