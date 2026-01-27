using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Requests
{
    public class DeleteSelectedSchedulesRequest : CommonRequest
    {
        public List<long> ListSchedules { get; set; } = new List<long>();
    }
}
