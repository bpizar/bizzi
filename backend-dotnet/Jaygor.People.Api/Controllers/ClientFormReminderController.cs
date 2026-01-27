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
    public class ClientFormReminderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ClientFormReminderController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallclientFormReminders")]
        public GetAllClientFormRemindersResponse GetAllClientFormReminders()
        {
            var response = new GetAllClientFormRemindersResponse();
            try
            {
                var clientFormRemindersAux = new List<ClientFormRemindersCustomEntity>();
                bussinnessLayer.GetAllClientFormReminders(out clientFormRemindersAux);
                response.ClientFormReminders = clientFormRemindersAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getclientformRemindersforeditbyid/{id}")]
        public GetClientFormReminderResponse GetClientFormReminderForEditById(long id)
        {
            var response = new GetClientFormReminderResponse();
            try
            {
                var clientFormReminderAux = new ClientFormRemindersCustomEntity();
                bussinnessLayer.GetClientFormReminderbyId(id, out clientFormReminderAux);
                response.ClientFormReminder = clientFormReminderAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteClientFormReminder/{id}")]
        public CommonResponse DeleteClientFormReminder(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteClientFormReminder(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveClientFormReminder")]
        public CommonResponse SaveClientFormReminder([FromBody]SaveClientFormReminderRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.SaveClientFormReminder(request.ClientFormReminder);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getallclientFormRemindersByClientForm/{idClientForm}")]
        public GetAllClientFormRemindersResponse GetAllClientFormReminders(long idClientForm)
        {
            var response = new GetAllClientFormRemindersResponse();
            try
            {
                var clientFormRemindersAux = new List<ClientFormRemindersCustomEntity>();
                bussinnessLayer.GetAllClientFormRemindersByClientForm(idClientForm, out clientFormRemindersAux);
                response.ClientFormReminders = clientFormRemindersAux;
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