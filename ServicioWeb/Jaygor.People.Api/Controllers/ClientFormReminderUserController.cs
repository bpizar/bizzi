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
    public class ClientFormReminderUserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ClientFormReminderUserController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallclientFormReminderUsers")]
        public GetAllClientFormReminderUsersResponse GetAllClientFormReminderUsers()
        {
            var response = new GetAllClientFormReminderUsersResponse();
            try
            {
                var clientFormReminderUsersAux = new List<ClientFormReminderUsersCustomEntity>();
                bussinnessLayer.GetAllClientFormReminderUsers(out clientFormReminderUsersAux);
                response.ClientFormReminderUsers = clientFormReminderUsersAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getclientformReminderUsersforeditbyid/{id}")]
        public GetClientFormReminderUserResponse GetClientFormReminderUserForEditById(long id)
        {
            var response = new GetClientFormReminderUserResponse();
            try
            {
                var clientFormReminderUserAux = new ClientFormReminderUsersCustomEntity();
                bussinnessLayer.GetClientFormReminderUserbyId(id, out clientFormReminderUserAux);
                response.ClientFormReminderUser = clientFormReminderUserAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteClientFormReminderUser/{id}")]
        public CommonResponse DeleteClientFormReminderUser(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteClientFormReminderUser(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveClientFormReminderUser")]
        public CommonResponse SaveClientFormReminderUser([FromBody]SaveClientFormReminderUserRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.SaveClientFormReminderUser(request.ClientFormReminderUser);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getallclientFormReminderUsersByClientFormReminder/{idClientFormReminder}")]
        public GetAllClientFormReminderUsersResponse GetAllClientFormReminderUsers(long idClientFormReminder)
        {
            var response = new GetAllClientFormReminderUsersResponse();
            try
            {
                var clientFormReminderUsersAux = new List<ClientFormReminderUsersCustomEntity>();
                bussinnessLayer.GetAllClientFormReminderUsersByClientFormReminder(idClientFormReminder, out clientFormReminderUsersAux);
                response.ClientFormReminderUsers = clientFormReminderUsersAux;
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