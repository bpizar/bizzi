using System;
using Microsoft.AspNetCore.Mvc;
using JayGor.People.Bussinness;
using Microsoft.AspNetCore.Authorization;
using JayGor.People.ErrorManager;
using JayGor.People.Entities.Responses;
using JayGor.People.Api.helpers;
using System.Linq;
using JayGor.People.DataAccess;

namespace Jaygor.People.Api.Controllers
{
	[Route("[controller]")]
    public class MobileController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public MobileController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }

		[Authorize(Roles = "user")]
		[HttpGet("getMySchedule")]
		public getSchedulebyIdResponse_Mobile getMySchedule()
		{
			var response = new getSchedulebyIdResponse_Mobile();

			try
			{
				var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;
				var user = bussinnessLayer.IdentityGetUserByEmail(userRequesting);

				response.Period = bussinnessLayer.GetLastActivePeriodAndDesc();
				response.Schedule = bussinnessLayer.GetScheduleByUser(user.Id);
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}

		[Authorize(Roles = "user")]
        [HttpGet("getSchedulebyUser/{id}")]
        public getSchedulebyIdResponse_Mobile getSchedulebyUser(long id)
		{
			var response = new getSchedulebyIdResponse_Mobile();

			try
			{
                response.Period = bussinnessLayer.GetLastActivePeriodAndDesc();
                response.Schedule = bussinnessLayer.GetScheduleByUser(id);
                response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}

        [Authorize(Roles = "user")]
		[HttpGet("GetProjectsPositionsbyUser/{idUser}")]
		public getUserProjectsPositionsResponse GetProjectsPositionsbyUser(long idUser)
		{
			var response = new getUserProjectsPositionsResponse();

            try
			{
                response.staffProjectPositions = bussinnessLayer.GetUserProjectsPositions(idUser);
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}

		[Authorize(Roles = "user")]
		[HttpGet("getMyProjectsPositions")]
		public getUserProjectsPositionsResponse GetMyProjectsPositions()
		{
			var response = new getUserProjectsPositionsResponse();

			var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;
			var user = bussinnessLayer.IdentityGetUserByEmail(userRequesting);

			try
			{
				response.staffProjectPositions = bussinnessLayer.GetUserProjectsPositions(user.Id);
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}

		[Authorize(Roles = "user")]
		[HttpGet("getMyTasks/{idUser}")]
        public GetMyTasksResponse_Mobile GetMyTasks_Mobile(long idUser)
		{
			var response = new GetMyTasksResponse_Mobile();

			try
			{
                //response.MyTasks = null;// bussinnessLayer.GetUserProjectsPositions_Mobile(id);

                var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;
                var user = bussinnessLayer.IdentityGetUserByEmail(userRequesting);

                response.MyTasks = bussinnessLayer.GetMyTasks_Mobile(user.Id);
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}

        [Authorize(Roles = "user")]
		[HttpGet("getProjectsDailyLogs/{idUser}")]
		public GetProjectsDailyLogsResponse_Mobile GetProjectsDailyLogs(long idUser)
		{
			var response = new GetProjectsDailyLogsResponse_Mobile();

			try
			{
				response.Projects = bussinnessLayer.GetProjectsDailyLogs_Mobile(idUser);
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}

        [Authorize(Roles = "user")]
		[HttpGet("getClientsDailyLogs/{idUser}")]
        public GetClientsDailyLogsResponse_Mobile GetClientsDailyLogs(long idUser)
		{
			var response = new GetClientsDailyLogsResponse_Mobile();

			try
			{
                response.Clients = bussinnessLayer.GetClientsDailyLogsResponse_Mobile(idUser);
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}

        [Authorize(Roles = "user")]
		[HttpGet("getTimeTrackerbyIdFromToday/{idUser}")]
		public GetTimeTrackerByIdFromTodayResponse_Mobile GetTimeTrackerbyIdFromToday(long idUser)
		{
			var response = new GetTimeTrackerByIdFromTodayResponse_Mobile();

			try
			{
                var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;
                var user = bussinnessLayer.IdentityGetUserByEmail(userRequesting);

                response.TimeTracker = bussinnessLayer.GetTimeTracker_Mobile(user.Id);
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}


        [Authorize(Roles = "user")]   	
        [HttpGet("getDailyLogsByIdUser/{idUser}")]
		public GetDailyLogsByUserIdResponse_Mobile GetDailyLogsByUserId_Mobile(long idUser)
		{
			var response = new GetDailyLogsByUserIdResponse_Mobile();

			try
			{
                response.DailyLogs = bussinnessLayer.GetDailyLogsByUserId_Mobile(idUser);
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}

        [Authorize(Roles = "user")]
		[HttpPost("saveAutoGeoTracking")]
        public CommonResponse SaveAutoGeoTracking(long idfUser,  float latitude,  float longitude)
        {
		var response = new CommonResponse();

			try
			{
				// response. = bussinnessLayer.GetDailyLogsByUserId_Mobile(idUser);
                response.Result = bussinnessLayer.SaveAutoGeoTracking(idfUser, latitude, longitude);
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;


        }


        [Authorize(Roles = "user")]        	
		[HttpPost("changetaskstate")]
        //public CommonResponse ChangeTaskState(long IdfState, long IdTask, long IdUser)
        public CommonResponse ChangeTaskState(long IdfState, long IdTask)
		{
			var response = new CommonResponse();

			try
			{
                var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;
                var user = bussinnessLayer.IdentityGetUserByEmail(userRequesting);

                response.Result = bussinnessLayer.ChangeTaskState(IdfState, IdTask, user.Id);
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}
            
        [Authorize(Roles = "user")]
		[HttpPost("starttimetracker")]
        public CommonResponse StartTimeTracker(long IdfStaffProjectPosition, string Note, float Longitude, float Latitude)
        {
			var response = new CommonResponse();

			try
			{
                //if (bussinnessLayer.CheckIfStaffProjectPositionInLastPeriod(IdfUser))
                //{
                    response.Result = bussinnessLayer.StartTimeTracker(IdfStaffProjectPosition, Note, Longitude, Latitude);
                //}
                //else
                //{
                 //   response.Result = true;
                 //   response.TagInfo = "needupdate";
                //}

			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
        }

        

        [Authorize(Roles = "user")]
		[HttpPost("stoptimetracker")]
        public CommonResponse StopTimeTracker(long Id, string Note, float Longitude, float Latitude)
		{
			var response = new CommonResponse();

			try
			{
				response.Result = bussinnessLayer.StopTimeTracker(Id,  Note, Longitude, Latitude);
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}

        [Authorize(Roles = "user")]
		[HttpPost("savedailylog")]
        public CommonResponse SaveDailyLog(long Id, 
                            long ProjectId, 
                            long ClientId,
                            long UserId,
                            string Placement,
                            string StaffOnShift,
                            string GeneralMood,
                            string InteractionStaff,
                            string InteractionPeers,
                            string School,
                            string Attended,
                            string InHouseProg,
                            string Comments,
                            string Health,
                            string ContactFamily,
                            string SeriousOccurrence,
                            string Other,
                            string State)
        {
			var response = new CommonResponse();

			try
			{
                //response.Result = bussinnessLayer.StopTimeTracker(Id, endNote, endLong, endLat);
                response.Result = bussinnessLayer.SaveDailyLog(Id, ProjectId, ClientId, UserId, Placement, StaffOnShift, GeneralMood, InteractionStaff, InteractionPeers, School, Attended, InHouseProg, Comments, Health, ContactFamily, SeriousOccurrence, Other, State);

			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
        }

		[Authorize(Roles = "user")]
        [Authorize(Roles = "faceRecorder")]
		[HttpPost("saveface")]
        public CommonResponse SaveFace(string email, string face)
        {
			var response = new CommonResponse();

			try
			{
				response.Result = bussinnessLayer.SaveFace(email, face);
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
        }

      


        //[HttpPost("addlocalreminder")]		
        //public CommonResponse SaveFace(string email, string face)
        //{
        //	var response = new CommonResponse();

        //	try
        //	{
        //		// response.Result = bussinnessLayer.SendMessage(idUser, idRoom, msg);              
        //	}
        //	catch (Exception ex)
        //	{
        //		response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
        //	}

        //	return response;
        //}


    }
}
