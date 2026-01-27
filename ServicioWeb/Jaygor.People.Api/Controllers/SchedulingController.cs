using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JayGor.People.Bussinness;
using Microsoft.AspNetCore.Authorization;
using JayGor.People.ErrorManager;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.DataAccess;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class SchedulingController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public SchedulingController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }

        [Authorize(Roles = "admin,viewer,schedulingeditor,user")]
        [HttpGet("getscheduling/{period}")]
        public GetSchedulingResponse GetScheduling(long period)
        {
            var response = new GetSchedulingResponse();

            // var userRequesting = HttpContext.User;
			var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;

            var SchedulingAux = new List<SchedulingCustomEntity>();
            var OverTimeAux = new List<OverTimeCustomEntity>();
            var StaffAux = new List<StaffForPlanningCustomEntity>();
            var overLapAux = new List<OverLapCustomEntity>();
            var projectsAux = new List<ProjectCustomEntity>(); 

            try
            {
                if (HttpContext.User.IsInRole("admin") || HttpContext.User.IsInRole("viewer"))
                {
                   bussinnessLayer.GetScheduling(period,
                       out SchedulingAux,
                       out OverTimeAux,
                       out overLapAux,
                       out StaffAux,
                       out projectsAux);
                    // response.Projects = bussinnessLayer.GetProjects().ToList();
                }
                else if (HttpContext.User.IsInRole("schedulingeditor"))
                {
                    bussinnessLayer.GetSchedulingByOwnProjects(period,
                                                               userRequesting ,
    														   out SchedulingAux,
    						                                   out OverTimeAux,
    						                                   out overLapAux,
    						                                   out StaffAux,
                                                 out projectsAux);
                    // response.Projects = bussinnessLayer.GetProjectsByUser(userRequesting).ToList();
                }
                else if (HttpContext.User.IsInRole("user"))
                {
                    bussinnessLayer.GetSchedulingByOwnScheduling(period, 
                                                                 userRequesting,
																 out SchedulingAux,
															     out OverTimeAux,
															     out overLapAux,
															     out StaffAux,
                                                 out projectsAux);

                    // response.Projects = bussinnessLayer.GetProjectsByUser(userRequesting).ToList();
                }

                response.Projects = projectsAux;
                response.Scheduling = SchedulingAux;
                response.OverTime = OverTimeAux;
                response.OverLap = overLapAux;
                response.Staffs = StaffAux;
			    response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("savescheduling")]
        public CommonResponse SaveScheduling([FromBody]SaveSchedulingRequest request)
        {
            var response = new CommonResponse();

            try
            {                
                response = bussinnessLayer.SaveScheduling(request.TimeDifference,
                                                          request.Period,
                                                          request.StaffsIds,
                                                          request.Date,
                                                          request.Time1,
                                                          request.Time2,
                                                          request.DuplicateScheduling);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("updatescheduling")]
        public CommonResponse UpdateScheduling([FromBody]UpdateSchedulingRequest request)
        {
            var response = new CommonResponse();

            try
            {
                response = bussinnessLayer.UpdateScheduling(request.TimeDifference, request.Id, request.Date, request.Time1, request.Time2);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("deletescheduling")]
        public CommonResponse DeleteScheduling([FromBody]DeleteSchedulingRequest request)
        {
            var response = new CommonResponse();

            try
            {
                response = bussinnessLayer.DeleteScheduling(request.Id);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }
        [HttpGet("getschedulingbyproject/{idproject}/{idperiod}")]
        public GetSchedulingResponse GetSchedulingByProject(long idproject,long idperiod)
        {
            var response = new GetSchedulingResponse();
			// var userRequesting = HttpContext.User;
			//var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;


			try
            {
                response.Scheduling = bussinnessLayer.GetSchedulingByProject(idproject,idperiod).ToList();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }
        [HttpGet("getschedulingbystaff/{idstaff}")]
        public GetSchedulingResponse GetSchedulingByStaff(long idstaff)
        {
            var response = new GetSchedulingResponse();
			//var userRequesting = HttpContext.User;
			//var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;


			try
            {
                response.Scheduling = bussinnessLayer.GetSchedulingByStaff(idstaff).ToList();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("deleteselectedschedules")]
        public CommonResponse DeleteSelectedSchedules([FromBody]DeleteSelectedSchedulesRequest request)
        {
            var response = new CommonResponse();

            try
            {                
                response.Result = bussinnessLayer.DeleteSelectedSchedules(request.ListSchedules);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }
    }
}
