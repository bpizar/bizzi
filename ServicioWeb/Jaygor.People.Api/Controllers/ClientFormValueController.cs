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
    public class ClientFormValueController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ClientFormValueController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallclientFormValues")]
        public GetAllClientFormValuesResponse GetAllClientFormValues()
        {
            var response = new GetAllClientFormValuesResponse();
            try
            {
                var clientFormValuesAux = new List<ClientFormValuesCustomEntity>();
                bussinnessLayer.GetAllClientFormValues(out clientFormValuesAux);
                response.ClientFormValues = clientFormValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getclientformValuesforeditbyid/{id}")]
        public GetClientFormValueResponse GetClientFormValueForEditById(long id)
        {
            var response = new GetClientFormValueResponse();
            try
            {
                var clientFormValueAux = new ClientFormValuesCustomEntity();
                bussinnessLayer.GetClientFormValuebyId(id, out clientFormValueAux);
                response.ClientFormValue = clientFormValueAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteClientFormValue/{id}")]
        public CommonResponse DeleteClientFormValue(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteClientFormValue(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveClientFormValue")]
        public CommonResponse SaveClientFormValue([FromBody]SaveClientFormValueRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.SaveClientFormValueWithDetail(request.ClientFormValue,request.ClientFormFieldValues);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getallclientFormValuesByClientFormAndClient/{idClientForm}/{idClient}")]
        public GetAllClientFormValuesResponse GetAllClientFormValues(long idClientForm, long idClient)
        {
            var response = new GetAllClientFormValuesResponse();
            try
            {
                var clientFormValuesAux = new List<ClientFormValuesCustomEntity>();
                bussinnessLayer.GetAllClientFormValuesByClientFormAndClient(idClientForm, idClient, out clientFormValuesAux);
                response.ClientFormValues = clientFormValuesAux;
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