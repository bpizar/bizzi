using JayGor.People.Entities.Entities;
using System;
using JayGor.People.Entities.Requests;


namespace JayGor.People.Entities.Requests
{
    public class MoveCopyTaskRequest : CommonRequest
    {
        public tasks Task { get; set; }
        public bool Move { get; set; }
        public long IdfProject { get; set; }
        public long IdfPeriod { get; set; }
	}
}
