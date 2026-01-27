using JayGor.People.Bussinness;
using JayGor.People.DataAccess;
using JayGor.People.Entities.Responses;
using JayGor.People.ErrorManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class FinanceController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public FinanceController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }

        [HttpGet("gettimetrackingreview/{idperiod}")]
        public GetTimeTrackingReviewResponse GetTimeTrackingReview(long idperiod)
        {
            var response = new GetTimeTrackingReviewResponse();

            try
            {
                response.Tracking = bussinnessLayer.GetTimeTrackingReviewResponse(idperiod).ToList();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }



        [HttpGet("gettimetrackingreviewbyprojectandperiod/{idperiod}/{idproject}")]
		public GetTimeTrackingReviewResponse GetTimeTrackingReviewByProjectAndPeriod(long idperiod,long idproject)
		{
			var response = new GetTimeTrackingReviewResponse();

			try
			{
                response.Tracking = bussinnessLayer.GetTimeTrackingReviewByProjectAndPeriodResponse(idperiod, idproject).ToList();
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}



        [HttpPost("savetimetrackingreview")]
        public CommonResponse SaveTimeTrackingReview([FromBody] SaveTimeTrackingReviewRequest request)
        {
            var response = new CommonResponse();

            try
            {
                response = bussinnessLayer.SaveTimeTrackingReview(request.Tracking);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


    }
}