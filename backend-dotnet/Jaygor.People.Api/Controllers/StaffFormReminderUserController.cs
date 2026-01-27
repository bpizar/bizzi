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
    public class StaffFormReminderUserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public StaffFormReminderUserController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallstaffFormReminderUsers")]
        public GetAllStaffFormReminderUsersResponse GetAllStaffFormReminderUsers()
        {
            var response = new GetAllStaffFormReminderUsersResponse();
            try
            {
                var staffFormReminderUsersAux = new List<StaffFormReminderUsersCustomEntity>();
                bussinnessLayer.GetAllStaffFormReminderUsers(out staffFormReminderUsersAux);
                response.StaffFormReminderUsers = staffFormReminderUsersAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getstaffformReminderUsersforeditbyid/{id}")]
        public GetStaffFormReminderUserResponse GetStaffFormReminderUserForEditById(long id)
        {
            var response = new GetStaffFormReminderUserResponse();
            try
            {
                var staffFormReminderUserAux = new StaffFormReminderUsersCustomEntity();
                bussinnessLayer.GetStaffFormReminderUserbyId(id, out staffFormReminderUserAux);
                response.StaffFormReminderUser = staffFormReminderUserAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteStaffFormReminderUser/{id}")]
        public CommonResponse DeleteStaffFormReminderUser(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteStaffFormReminderUser(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveStaffFormReminderUser")]
        public CommonResponse SaveStaffFormReminderUser([FromBody]SaveStaffFormReminderUserRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.SaveStaffFormReminderUser(request.StaffFormReminderUser);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getallstaffFormReminderUsersByStaffFormReminder/{idStaffFormReminder}")]
        public GetAllStaffFormReminderUsersResponse GetAllStaffFormReminderUsers(long idStaffFormReminder)
        {
            var response = new GetAllStaffFormReminderUsersResponse();
            try
            {
                var staffFormReminderUsersAux = new List<StaffFormReminderUsersCustomEntity>();
                bussinnessLayer.GetAllStaffFormReminderUsersByStaffFormReminder(idStaffFormReminder, out staffFormReminderUsersAux);
                response.StaffFormReminderUsers = staffFormReminderUsersAux;
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