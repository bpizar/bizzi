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
    public class StaffFormReminderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public StaffFormReminderController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallstaffFormReminders")]
        public GetAllStaffFormRemindersResponse GetAllStaffFormReminders()
        {
            var response = new GetAllStaffFormRemindersResponse();
            try
            {
                var staffFormRemindersAux = new List<StaffFormRemindersCustomEntity>();
                bussinnessLayer.GetAllStaffFormReminders(out staffFormRemindersAux);
                response.StaffFormReminders = staffFormRemindersAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getstaffformRemindersforeditbyid/{id}")]
        public GetStaffFormReminderResponse GetStaffFormReminderForEditById(long id)
        {
            var response = new GetStaffFormReminderResponse();
            try
            {
                var staffFormReminderAux = new StaffFormRemindersCustomEntity();
                bussinnessLayer.GetStaffFormReminderbyId(id, out staffFormReminderAux);
                response.StaffFormReminder = staffFormReminderAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteStaffFormReminder/{id}")]
        public CommonResponse DeleteStaffFormReminder(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteStaffFormReminder(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveStaffFormReminder")]
        public CommonResponse SaveStaffFormReminder([FromBody]SaveStaffFormReminderRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.SaveStaffFormReminder(request.StaffFormReminder);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getallstaffFormRemindersByStaffForm/{idStaffForm}")]
        public GetAllStaffFormRemindersResponse GetAllStaffFormReminders(long idStaffForm)
        {
            var response = new GetAllStaffFormRemindersResponse();
            try
            {
                var staffFormRemindersAux = new List<StaffFormRemindersCustomEntity>();
                bussinnessLayer.GetAllStaffFormRemindersByStaffForm(idStaffForm, out staffFormRemindersAux);
                response.StaffFormReminders = staffFormRemindersAux;
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