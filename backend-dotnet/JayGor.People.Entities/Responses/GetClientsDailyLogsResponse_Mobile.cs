using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetClientsDailyLogsResponse_Mobile : CommonResponse
    {
       public List<ClientsDailyLogs_Mobile> Clients { get; set; }
    }
}



