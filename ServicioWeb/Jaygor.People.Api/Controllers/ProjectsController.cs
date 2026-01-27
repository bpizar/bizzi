using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JayGor.People.Bussinness;
using Microsoft.AspNetCore.Authorization;
using JayGor.People.ErrorManager;
using JayGor.People.Entities.Responses;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.CustomEntities;
using Jaygor.People.Api.helpers;
using JayGor.People.Api.helpers;
using JayGor.People.DataAccess;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class ProjectsController : Microsoft.AspNetCore.Mvc.Controller
    {
        //private readonly BussinnessLayer bussinnessLayer = new BussinnessLayer();
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public ProjectsController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }

        [Authorize(Roles = "admin,projecteditor")]
        [HttpGet("getprojects")]
        public GetProjectsResponse GetProjects()
        {
            var response = new GetProjectsResponse { Projects = new List<ProjectCustomEntity>() };
            // var userRequesting = HttpContext.User;
            // var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;

            var userRequesting = HttpContext.User.Claims.Single(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                if (HttpContext.User.IsInRole("admin") || HttpContext.User.IsInRole("viewer"))
                {
                    response.Projects = bussinnessLayer.GetProjects().ToList();
                }
                else if (HttpContext.User.IsInRole("projecteditor"))
                {
                    response.Projects = bussinnessLayer.GetProjectsByUser(userRequesting).ToList();
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getprojectslist")]
        public GetProjectsListResponse GetProjectsList()
        {
            var response = new GetProjectsListResponse { Projects = new List<ProjectListCustomEntity>() };
            // var userRequesting = HttpContext.User;
            // var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;

            var userRequesting = HttpContext.User.Claims.Single(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                if (HttpContext.User.IsInRole("admin") || HttpContext.User.IsInRole("viewer"))
                {
                    bussinnessLayer.GetProjects().ToList().ForEach(p => {
                        response.Projects.Add(new ProjectListCustomEntity()
                        {
                            Id = p.Id,
                            ProjectName = p.ProjectName,
                            Description = p.Description,
                            State = p.State,
                            Color = p.Color,
                            BeginDate = p.BeginDate,
                            EndDate = p.EndDate,
                            Visible = p.Visible,
                            TotalHours = p.TotalHours
                        });
                    });
                }
                else if (HttpContext.User.IsInRole("projecteditor"))
                {
                    bussinnessLayer.GetProjectsByUser(userRequesting).ToList().ForEach(p => {
                        response.Projects.Add(new ProjectListCustomEntity()
                        {
                            Id = p.Id,
                            ProjectName = p.ProjectName,
                            Description = p.Description,
                            State = p.State,
                            Color = p.Color,
                            BeginDate = p.BeginDate,
                            EndDate = p.EndDate,
                            Visible = p.Visible,
                            TotalHours = p.TotalHours
                        });
                    });
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [Authorize(Roles = "admin,manager")]
        [Authorize(Roles = "admin,viewer,projecteditor")]
        [HttpGet("getproject/{id}")]
        public GetProjectResponse GetProject(long id)
        {
            var response = new GetProjectResponse { Project = new ProjectCustomEntity() };

            try
            {
                var projectAux = new ProjectCustomEntity();

                //var clientsAux = new List<ClientCustomEntity>();


                var settingsReminderTimeAux = new List<settings_reminder_time>();


                var clientsAllPeriodsAux = new List<ClientCustomEntity>();



                response.Positions = bussinnessLayer.GetPositions().ToList();
                bussinnessLayer.GetProject(id, out projectAux, out settingsReminderTimeAux, out clientsAllPeriodsAux);
                response.Project = projectAux;

                response.clientsAllPeriods = clientsAllPeriodsAux;


                response.SettingsReminderTime = settingsReminderTimeAux;
                //response.Clients = clientsAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getproject2/{id}/{idperiod}")]
        public GetProject2Response GetProject2(long id, long idperiod)
        {
            var response = new GetProject2Response();
            // return response;

            try
            {
                var taskAux = new List<TasksForPlanningCustomEntity>();
                var staffAux = new List<StaffCustomEntity>();
                var TasksRemindersAux = new List<tasks_reminders>();
                var clientsAux = new List<ClientCustomEntity>();
                var ownersAux = new List<ProjectOwnersCustom>();
                //var staffPeriodSettingsAux = new List<staff_period_settings>();

                long hoursAux = 0;

                bussinnessLayer.GetProject2(id, idperiod, out taskAux, out staffAux, out hoursAux, out TasksRemindersAux, out clientsAux);

                response.Clients = clientsAux;
                response.Tasks = taskAux;
                response.Staffs = staffAux;
                response.TasksHours = hoursAux;
                response.TasksReminders = TasksRemindersAux;
                //response.StaffPeriodSettings = staffPeriodSettingsAux;

                response.StaffsForOwners = bussinnessLayer.GetStaffsForOwnerList(id, idperiod, out ownersAux).ToList();

                response.Owners = ownersAux;

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }



        // public GetProject2Response Response { get; set; } = new GetProject2Response();  
        [Authorize(Roles = "admin,projecteditor")]
        [HttpPost("saveproject")]
        public GetProject2Response SaveProject([FromBody]SaveProjectRequest request)
        {
            var response = new GetProject2Response();
            // var userRequesting = HttpContext.User;

            var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;

            try
            {
                var tasksOut = new List<TasksForPlanningCustomEntity>();

                var staffsOut = new List<StaffCustomEntity>();
                var taskHoursOut = 0L;
                var taskRemindersOut = new List<tasks_reminders>();
                var clientsOut = new List<ClientCustomEntity>();
                var returningData = false;
                var TasksRemindersAux = new List<tasks_reminders>();

                response.TagInfo = bussinnessLayer.SaveProject(request.Project,
                                                               request.Tasks,
                                                               request.Staffs,
                                                               request.Owners,
                                                               request.tasksReminders,
                                                               userRequesting,
                                                               request.IdPeriod,
                                                                HttpContext.User.IsInRole("admin"),
                                                               request.clients,
                                                                out TasksRemindersAux,
                                                                out tasksOut,
                                                                out staffsOut,
                                                                out taskHoursOut,
                                                                out taskRemindersOut,
                                                                out clientsOut,
                                                                request.ForceGetData,
                                                                out returningData
                                                                  ).ToString();

                response.Tasks.AddRange(tasksOut);
                response.Staffs.AddRange(staffsOut);
                response.Clients.AddRange(clientsOut);
                response.ForceLoad = returningData;
                response.TasksReminders.AddRange(TasksRemindersAux);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }



        [Authorize(Roles = "admin")]
        [HttpPost("deleteproject")]
        public CommonResponse DeleteProject([FromBody]SaveProjectRequest request)
        {
            var response = new CommonResponse();

            var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;

            try
            {
                response.Result = bussinnessLayer.DeleteProject(request.Project, request.IdPeriod);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [Authorize(Roles = "admin,projecteditor")]
        [HttpGet("getpettycash/{id}/{idperiod}/{idcategory}")]
        public GetPettyCashResponse GetPettyCash(long id, long idperiod, long idcategory)
        {
            var response = new GetPettyCashResponse();
            // var userRequesting = HttpContext.User;

            try
            {
                response.PettyCash = bussinnessLayer.GetPettyCash(id, idperiod, idcategory);
                response.PettyCashCategories = bussinnessLayer.GetPettyCashCategories();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


        [Authorize(Roles = "admin,projecteditor")]
        [HttpGet("getpettycashcategories")]
        public GetPettyCashCategoriesResponse GetPettyCashCategories()
        {
            var response = new GetPettyCashCategoriesResponse();
            // var userRequesting = HttpContext.User;

            try
            {
                response.Categories = bussinnessLayer.GetPettyCashCategories().ToList();
                //response.PettyCash = bussinnessLayer.GetPettyCash(id, idperiod);
                //response.PettyCashCategories = bussinnessLayer.GetPettyCashCategories();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [Authorize(Roles = "admin,projecteditor")]
        [HttpPost("savepettycash")]
        public CommonResponse SavePettyCash([FromBody]SavePettyCashRequest request)
        {
            var response = new CommonResponse();
            //var userRequesting = HttpContext.User;

            try
            {
                var uername = string.Empty;
                response = bussinnessLayer.SavePettyCash(request.PettyCash, uername, request.IdPeriod);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


        [HttpGet("getstaffandpositionsforcopywindow/{id}/{idperiod}")]
        public GetStaffAndPositionsForCopyWindowResponse GetPrGetStaffAndPositionsForCopyWindowoject2(long id, long idperiod)
        {
            var response = new GetStaffAndPositionsForCopyWindowResponse();

            try
            {
                var staffAux = new List<StaffCustomEntity>();
                var positionsAux = new List<positions>();

                bussinnessLayer.GetStaffAndPositionsForCopyWindow(id, idperiod, out staffAux, out positionsAux);

                response.Staffs = staffAux;
                response.Positions = positionsAux;
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
