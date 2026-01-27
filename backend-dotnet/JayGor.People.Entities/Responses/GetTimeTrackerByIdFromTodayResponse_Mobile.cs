using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetTimeTrackerByIdFromTodayResponse_Mobile : CommonResponse
    {
        public List<TimeTracker_Mobile> TimeTracker { get; set; }
    }
}


