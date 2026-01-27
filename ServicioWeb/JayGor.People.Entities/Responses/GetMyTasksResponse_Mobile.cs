using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetMyTasksResponse_Mobile : CommonResponse
    {
        public List<MyTasks_Mobile> MyTasks { get; set; }
    }
}