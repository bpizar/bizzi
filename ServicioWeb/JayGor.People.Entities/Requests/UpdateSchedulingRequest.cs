using System;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class UpdateSchedulingRequest : CommonRequest
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time1 { get; set; }
        public DateTime Time2 { get; set; }
        //public bool FixTimeZone { get; set; }

        public long TimeDifference { get; set; }
    }
}