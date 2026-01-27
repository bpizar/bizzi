using System;
using System.Collections.Generic;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class GetReport1Request : CommonRequest
    {
        public List<long> ProjectIds { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}