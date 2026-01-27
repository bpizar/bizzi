using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveTimeTrackingReviewRequest : CommonRequest
    {
        public List<TimeTrackingReviewCustom> Tracking { get; set; } = new List<TimeTrackingReviewCustom>();
    }
}