using System;

namespace JayGor.People.Entities.Requests
{
    public class GetGeoTrackingRequest:CommonRequest
    {       
			public long IdPeriod { get; set; }
    		public DateTime Datex { get; set; }    		
            public bool GetAuto { get; set; }
    }
}