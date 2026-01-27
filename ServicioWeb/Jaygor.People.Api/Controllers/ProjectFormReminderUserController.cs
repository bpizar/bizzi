using JayGor.People.Bussinness;
using JayGor.People.Entities.Responses;
using JayGor.People.ErrorManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Api.helpers;
using Jaygor.People.Api.helpers;
using JayGor.People.DataAccess;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class ProjectFormReminderUserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ProjectFormReminderUserController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallprojectFormReminderUsers")]
        public GetAllProjectFormReminderUsersResponse GetAllProjectFormReminderUsers()
        {
            var response = new GetAllProjectFormReminderUsersResponse();
            try
            {
                var projectFormReminderUsersAux = new List<ProjectFormReminderUsersCustomEntity>();
                bussinnessLayer.GetAllProjectFormReminderUsers(out projectFormReminderUsersAux);
                response.ProjectFormReminderUsers = projectFormReminderUsersAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getprojectformReminderUsersforeditbyid/{id}")]
        public GetProjectFormReminderUserResponse GetProjectFormReminderUserForEditById(long id)
        {
            var response = new GetProjectFormReminderUserResponse();
            try
            {
                var projectFormReminderUserAux = new ProjectFormReminderUsersCustomEntity();
                bussinnessLayer.GetProjectFormReminderUserbyId(id, out projectFormReminderUserAux);
                response.ProjectFormReminderUser = projectFormReminderUserAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteProjectFormReminderUser/{id}")]
        public CommonResponse DeleteProjectFormReminderUser(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteProjectFormReminderUser(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveProjectFormReminderUser")]
        public CommonResponse SaveProjectFormReminderUser([FromBody]SaveProjectFormReminderUserRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.SaveProjectFormReminderUser(request.ProjectFormReminderUser);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getallprojectFormReminderUsersByProjectFormReminder/{idProjectFormReminder}")]
        public GetAllProjectFormReminderUsersResponse GetAllProjectFormReminderUsers(long idProjectFormReminder)
        {
            var response = new GetAllProjectFormReminderUsersResponse();
            try
            {
                var projectFormReminderUsersAux = new List<ProjectFormReminderUsersCustomEntity>();
                bussinnessLayer.GetAllProjectFormReminderUsersByProjectFormReminder(idProjectFormReminder, out projectFormReminderUsersAux);
                response.ProjectFormReminderUsers = projectFormReminderUsersAux;
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