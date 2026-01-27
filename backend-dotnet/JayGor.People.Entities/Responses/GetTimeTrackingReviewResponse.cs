using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetTimeTrackingReviewResponse : CommonResponse
    {
        public List<TimeTrackingReviewCustom> Tracking { get; set; } = new List<TimeTrackingReviewCustom>();
    }
}
