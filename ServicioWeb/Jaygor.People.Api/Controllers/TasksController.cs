using System;
using System.Collections.Generic;
using System.Linq;
using Jaygor.People.Api.helpers;
using JayGor.People.Bussinness;
using JayGor.People.DataAccess;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;
using JayGor.People.Entities.Responses;
using JayGor.People.ErrorManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class TasksController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public TasksController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("savetask")]
        public CommonResponse SaveTask([FromBody]SaveTaskRequest request)
        {
            var response = new CommonResponse();

            try
            {
                response.Result = bussinnessLayer.SaveTask(request.Task,request.DuplicateTask,request.EditingSerie);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            
            return response;
        }
        ////[Authorize(Roles = "admin")]
        //[HttpPost("clonetask")]
        //public CommonResponse CloneTask([FromBody]CloneTaskRequest request)
        //{
        //    var response = new CommonResponse();

        //    try
        //    {
        //        response.Result = bussinnessLayer.CloneTask(request.Id, request.IdfProject, request.IdfPeriod,true);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
        //    }

        //    return response;
        //}

        [HttpPost("movecopytask")]
        public CommonResponse MoveTask([FromBody]MoveCopyTaskRequest request)
        {
            var response = new CommonResponse();

            try
            {
                response.Result = bussinnessLayer.MoveCopyTask(request.Task, request.Move, request.IdfProject, request.IdfPeriod);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        //TODO:DEPRECATED.
        [HttpGet("gettask/{id}")]
        public GetTaskResponse GetTask(long id)
        {
            var response = new GetTaskResponse();

            try
            {
                //response.Tasks = new List<Entities.Entities.Task_>();
                //response.Tasks.Add((Entities.Entities.Task_)bussinnessLayer.GetTask((long)id));
                //response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }
        
        [HttpGet("gettasksbystaff/{id}/{idperiod}")]
        public GetTasksByStaffResponse GetTasksByStaff(long id, long idperiod)
        {
            var response = new GetTasksByStaffResponse();

            try
            {
                var tasksAux = new List<TasksForPlanningCustomEntity>();
                long AssignedHoursOnPeriodAux = 0;
                long AvailableHoursOnPeriodAux = 0;
                string AssignedProgramsToAux = string.Empty;


                var  staffPeriodSettingsAux = new staff_period_settings();
                var workingHoursByPeriodStaff = Convert.ToInt32(CommonHelper.StaffWorkingHourDefault());

                bussinnessLayer.GetTasksByStaff(id, idperiod, workingHoursByPeriodStaff, out tasksAux, out AssignedHoursOnPeriodAux, out AvailableHoursOnPeriodAux, out staffPeriodSettingsAux, out AssignedProgramsToAux);

                response.AssignedHoursOnPeriod = AssignedHoursOnPeriodAux;
                response.AvailableHoursOnPeriod = AvailableHoursOnPeriodAux;
                response.AssignedTasks = tasksAux;
                response.AssignedPrograms = AssignedProgramsToAux;

                response.StaffPeriodSettings = staffPeriodSettingsAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }
        [HttpGet("getstatuses")]
        public GetStatusesResponse GetStatuses()
        {
            var response = new GetStatusesResponse { StatusesList = new List<statuses>() };
            //var userRequesting = HttpContext.User;

            try
            {
                //if (userRequesting.IsInRole("admin"))
                //{
                response.StatusesList = bussinnessLayer.GetStatuses().ToList();
                //}
                //else if (userRequesting.IsInRole("projecteditor"))
                //{
                //    response.Projects = bussinnessLayer.GetProjectsByUser(userRequesting.Identity.Name).ToList();
                //}

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }
        [HttpGet("gettasks/{from}/{to}")]
        public GetTaskResponse GetTasks(DateTime from, DateTime to)
        {
            var response = new GetTaskResponse { Tasks = new List<TasksForPlanningCustomEntity>() };

            try
            {
                var tasksAux = new List<TasksForPlanningCustomEntity>();

                var unassignedTasksAux = new List<TasksForPlanningCustomEntity>();

                var duplicatesAux = new List<duplicate_tasks>();

                bussinnessLayer.GetTasks(from, 
                                         to,
                                         out tasksAux, 
                                         out duplicatesAux,
                                         out unassignedTasksAux);

                response.Tasks = tasksAux;
                response.Duplicates = duplicatesAux;
                response.UnAssignedTasks = unassignedTasksAux;

                response.Result = true;                                                                 
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

		//[HttpGet("gettasksbyproject/{idproject}")]
		//public GetTaskResponse GetTasksByProject(int idProject)
		//{
		//    var response = new GetTaskResponse();

		//    try
		//    {
		//        var tasksAux = new List<TasksForPlanningCustomEntity>();

		//        bussinnessLayer.GetTasksByProject(idProject,out tasksAux);

		//        response.Tasks = tasksAux;
		//        response.Result = true;
		//    }
		//    catch (Exception ex)
		//    {
		//        response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
		//    }

		//    return response;
		//}

		[HttpGet("gettasksbyproject/{idproject}/{idperiod}")]
		public GetTaskResponse GetTasksByProject(long idProject,long idPeriod)
		{
			var response = new GetTaskResponse();

			try
			{
				var tasksAux = new List<TasksForPlanningCustomEntity>();

                bussinnessLayer.GetTasksByProject(idProject, idPeriod, out tasksAux);

				response.Tasks = tasksAux;
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}


        [HttpGet("getoverduetasks")]
        public GetTaskResponse GetOverdueTasks()
        {
            var response = new GetTaskResponse();

            try
            {
                var tasksAux = new List<TasksForPlanningCustomEntity>();

                bussinnessLayer.GetOverdueTasks(out tasksAux);

                response.Tasks = tasksAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


        //// public List<GenericPair> GetTasksForDashboard1(out string periodDescription)
        //[HttpGet("gettasksfordashboard1")]
        //public CommonResponse GetTasksForDashboard1()
        //{
        //	var response = new CommonResponse();

        //	try
        //	{
        //              string periodDesc;
        //              response.Messages = bussinnessLayer.GetTasksForDashboard1(out periodDesc);
        //              response.TagInfo = periodDesc;
        //		response.Result = true;
        //	}
        //	catch (Exception ex)
        //	{
        //		response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
        //	}

        //	return response;
        // }


       [Authorize(Roles = "user")]
       [HttpGet("getremindersforpanel")]
       public GetRemindersForPanelResponse GetRemindersForPanel()
       {
            var response = new GetRemindersForPanelResponse();
            var userRequesting = HttpContext.User.Claims.Single(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                var ReminderTodayAux  = new List<ReminderForPanel>();
                var RemindersTomorrowAux = new List<ReminderForPanel>();
                var RemindersOthersAux  = new List<ReminderForPanel>();
                var RemindersMedicalsAux = new List<ReminderForPanel>();

                response.Result = bussinnessLayer.GetRemindersForPanel(out ReminderTodayAux, out RemindersTomorrowAux, out RemindersOthersAux, out RemindersMedicalsAux, userRequesting);

                response.ReminderToday.AddRange(ReminderTodayAux);
                response.RemindersTomorrow.AddRange(RemindersTomorrowAux);
                response.RemindersOthers.AddRange(RemindersOthersAux);
                response.RemindersMedicals.AddRange(RemindersMedicalsAux);

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        //[HttpPost("savewebonesignalid/{idonesignal}")]
        // public CommonResponse SaveWebOneSignalId(string idonesignal)
        // {
        //     var response = new CommonResponse();
        //     var userRequesting = HttpContext.User.Claims.Single(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
 
        //     try
        //     {
        //         var client = bussinnessLayer.IdentityGetUserByEmail(userRequesting);
        //         response.Result = bussinnessLayer.IdentityUpdateIdOneSignalBrowser(client.Id, idonesignal);
        //     }
        //     catch (Exception ex)
        //     {
        //         response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
        //     }

        //     return response;
        //  }

    }
}