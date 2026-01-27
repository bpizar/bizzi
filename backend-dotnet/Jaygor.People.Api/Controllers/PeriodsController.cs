using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JayGor.People.Bussinness;
using Microsoft.AspNetCore.Authorization;
using JayGor.People.ErrorManager;
using JayGor.People.Entities.Responses;
using Jaygor.People.Api.helpers;
using JayGor.People.DataAccess;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class PeriodsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public PeriodsController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }


        [HttpGet("getperiods")]
        public GetPeriodsResponse GetPeriods()
        {
            var response = new GetPeriodsResponse();

            try
            {
                response.PeriodsList = bussinnessLayer.GetPeriods().ToList();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("saveperiods")]
        public CommonResponse SavePeriods([FromBody]SavePeriodsRequest request)
        {
            var response = new CommonResponse();

            try
            {


				var workingHoursByPeriodStaff = Convert.ToInt32(CommonHelper.StaffWorkingHourDefault());


				response = bussinnessLayer.SavePeriods(request.Periods, workingHoursByPeriodStaff);
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