using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveSchedulingRequest : CommonRequest
    {

		public DateTime Date { get; set; }
		public duplicate_scheduling DuplicateScheduling { get; set; }
        public long Period { get; set; }
        public List<long> StaffsIds { get; set; }
       
        public DateTime Time1 { get; set; }
        public DateTime Time2 { get; set; }

        public long TimeDifference { get; set; }

    }
}