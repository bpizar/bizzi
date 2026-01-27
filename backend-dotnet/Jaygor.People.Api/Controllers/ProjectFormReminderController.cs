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
    public class ProjectFormReminderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ProjectFormReminderController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallprojectFormReminders")]
        public GetAllProjectFormRemindersResponse GetAllProjectFormReminders()
        {
            var response = new GetAllProjectFormRemindersResponse();
            try
            {
                var projectFormRemindersAux = new List<ProjectFormRemindersCustomEntity>();
                bussinnessLayer.GetAllProjectFormReminders(out projectFormRemindersAux);
                response.ProjectFormReminders = projectFormRemindersAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getprojectformRemindersforeditbyid/{id}")]
        public GetProjectFormReminderResponse GetProjectFormReminderForEditById(long id)
        {
            var response = new GetProjectFormReminderResponse();
            try
            {
                var projectFormReminderAux = new ProjectFormRemindersCustomEntity();
                bussinnessLayer.GetProjectFormReminderbyId(id, out projectFormReminderAux);
                response.ProjectFormReminder = projectFormReminderAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteProjectFormReminder/{id}")]
        public CommonResponse DeleteProjectFormReminder(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteProjectFormReminder(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveProjectFormReminder")]
        public CommonResponse SaveProjectFormReminder([FromBody]SaveProjectFormReminderRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.SaveProjectFormReminder(request.ProjectFormReminder);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getallprojectFormRemindersByProjectForm/{idProjectForm}")]
        public GetAllProjectFormRemindersResponse GetAllProjectFormReminders(long idProjectForm)
        {
            var response = new GetAllProjectFormRemindersResponse();
            try
            {
                var projectFormRemindersAux = new List<ProjectFormRemindersCustomEntity>();
                bussinnessLayer.GetAllProjectFormRemindersByProjectForm(idProjectForm, out projectFormRemindersAux);
                response.ProjectFormReminders = projectFormRemindersAux;
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