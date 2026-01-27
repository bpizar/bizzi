using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        private long TimeFormatToSeconds(string time)
        {
            var hours = long.Parse(time.Substring(0, 2));
            var mins = long.Parse(time.Substring(3, 2));
            var secs = long.Parse(time.Substring(6, 2));
            return secs + mins * 60 + hours * 3600;
        }

        private string SecondsToTimeFormat(double secs)
        {
            var hours = Math.Floor(Convert.ToDecimal(secs / (60 * 60)));
            var divisor_for_minutes = secs % (60 * 60);
            var minutes = Math.Floor(Convert.ToDecimal(divisor_for_minutes / 60));
            var divisor_for_seconds = divisor_for_minutes % 60;
            var seconds = Math.Ceiling(Convert.ToDecimal(divisor_for_seconds));
            var h2 = hours < 10 ? '0' + hours.ToString() : hours.ToString();
            var m2 = minutes < 10 ? '0' + minutes.ToString() : minutes.ToString();
            var s2 = seconds < 10 ? '0' + seconds.ToString() : seconds.ToString();
            return h2 + ':' + m2 + ':' + s2;
        }

        public IEnumerable<TimeTrackingReviewCustom> GetTimeTrackingReviewResponse(long idPeriod)
        {
            var result = dataAccessLayer.GetTimeTrackingReviewResponse(idPeriod);
            
            foreach (var r in result)
            {
                r.ScheduledTimeFormat = this.SecondsToTimeFormat(r.SecondsScheduledTime);
                r.UserTrackingFormat = this.SecondsToTimeFormat(r.SecondsUserTracking);
                r.ModifiedTrackingFormat = this.SecondsToTimeFormat(r.SecondsModifiedTracking);
            }

            return result;
        }


		public IEnumerable<TimeTrackingReviewCustom> GetTimeTrackingReviewByProjectAndPeriodResponse(long idPeriod,long idproject)
		{
			var result = dataAccessLayer.GetTimeTrackingReviewResponse(idPeriod);

			foreach (var r in result)
			{
				r.ScheduledTimeFormat = this.SecondsToTimeFormat(r.SecondsScheduledTime);
				r.UserTrackingFormat = this.SecondsToTimeFormat(r.SecondsUserTracking);
				r.ModifiedTrackingFormat = this.SecondsToTimeFormat(r.SecondsModifiedTracking);
			}

            //var resultAux = new List<TimeTrackingReviewCustom>();

            var resultAux = result.Where(c => c.IdfProject == idproject);

            return resultAux!=null ? resultAux.ToList() : new List<TimeTrackingReviewCustom>();  
		}



        public CommonResponse SaveTimeTrackingReview(List<TimeTrackingReviewCustom> tracking)
        {
            tracking.ForEach(c => c.SecondsModifiedTracking = this.TimeFormatToSeconds(c.ModifiedTrackingFormat));
            return dataAccessLayer.SaveTimeTrackingReview(tracking);
        }
    }
}
