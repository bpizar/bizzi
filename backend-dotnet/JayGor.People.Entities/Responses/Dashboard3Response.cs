
using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class Dashboard3Response : CommonResponse
    {
        // public List<TimeTrackingReviewCustom> Tracking { get; set; } = new List<TimeTrackingReviewCustom>();
        //public string Group { get; set; }
        //public string Value1 { get; set; }
        //public string Value2 { get; set; }

        public List<GenericTriValue> Values { get; set; }

        //public List<string> ProjectNames { get; set; }
    }
}