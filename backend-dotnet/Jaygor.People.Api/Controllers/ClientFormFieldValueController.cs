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
    public class ClientFormFieldValueController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ClientFormFieldValueController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallclientFormFieldValues")]
        public GetAllClientFormFieldValuesResponse GetAllClientFormFieldValues()
        {
            var response = new GetAllClientFormFieldValuesResponse();

            try
            {
                var clientFormFieldValuesAux = new List<ClientFormFieldValuesCustomEntity>();
                bussinnessLayer.GetAllClientFormFieldValues(out clientFormFieldValuesAux);
                response.ClientFormFieldValues = clientFormFieldValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getclientformfieldValuesforeditbyid/{id}")]
        public GetClientFormFieldValueResponse GetClientFormFieldValueForEditById(long id)
        {
            var response = new GetClientFormFieldValueResponse();
            try
            {
                var clientFormFieldValueAux = new ClientFormFieldValuesCustomEntity();
                bussinnessLayer.GetClientFormFieldValuebyId(id, out clientFormFieldValueAux);
                response.ClientFormFieldValue = clientFormFieldValueAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("deleteClientFormFieldValue/{id}")]
        public CommonResponse DeleteClientFormFieldValue(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteClientFormFieldValue(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveclientformfieldvalue")]
        public CommonResponse SaveClientFormFieldValue([FromBody]SaveClientFormFieldValueRequest request)
        {
            var response = new CommonResponse();
            try
            {
                var isNew = request.ClientFormFieldValue.Id == -1;
                response = bussinnessLayer.SaveClientFormFieldValue(request.ClientFormFieldValue);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallclientFormFieldValuesByClientFormAndClient/{idClientForm}/{idClient}")]
        public GetAllClientFormFieldValuesResponse GetAllClientFormFieldValues(long idClientForm, long idClient)
        {
            var response = new GetAllClientFormFieldValuesResponse();
            try
            {
                var clientFormFieldValuesAux = new List<ClientFormFieldValuesCustomEntity>();
                bussinnessLayer.GetAllClientFormFieldValuesByClientFormAndClient(idClientForm, idClient, out clientFormFieldValuesAux);
                response.ClientFormFieldValues = clientFormFieldValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }
        
        [HttpGet("getallclientFormFieldValuesByClientFormValue/{idClientFormValue}")]
        public GetAllClientFormFieldValuesResponse GetAllClientFormFieldValues(long idClientFormValue)
        {
            var response = new GetAllClientFormFieldValuesResponse();
            try
            {
                var clientFormFieldValuesAux = new List<ClientFormFieldValuesCustomEntity>();
                bussinnessLayer.GetAllClientFormFieldValuesByClientFormValue(idClientFormValue, out clientFormFieldValuesAux);
                response.ClientFormFieldValues = clientFormFieldValuesAux;
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