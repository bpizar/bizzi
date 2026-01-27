using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class Dashboard2Response : CommonResponse
    {
        // public List<TimeTrackingReviewCustom> Tracking { get; set; } = new List<TimeTrackingReviewCustom>();
        public long MaxValue { get; set; }
        public List<string> Colors { get; set; }
        public List<long> Values { get; set; }
        public List<string> ProjectNames { get; set; }
    }
}