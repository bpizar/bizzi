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
    public class ClientFormFieldController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ClientFormFieldController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallclientFormFields")]
        public GetAllClientFormFieldsResponse GetAllClientFormFields()
        {
            var response = new GetAllClientFormFieldsResponse();

            try
            {
                var clientFormFieldsAux = new List<ClientFormFieldsCustomEntity>();
                bussinnessLayer.GetAllClientFormFields(out clientFormFieldsAux);
                response.ClientFormFields = clientFormFieldsAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getclientformfieldsforeditbyid/{id}")]
        public GetClientFormFieldResponse GetClientFormFieldForEditById(long id)
        {
            var response = new GetClientFormFieldResponse();

            try
            {
                var clientFormFieldAux = new ClientFormFieldsCustomEntity();
                var rolesAux = new List<Identity_RolesCustom>();

                bussinnessLayer.GetClientFormFieldbyId(id, out clientFormFieldAux);

                response.ClientFormField = clientFormFieldAux;
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("deleteClientFormField/{id}")]
        public CommonResponse DeleteClientFormField(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteClientFormField(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveclientformfield")]
        public CommonResponse SaveClientFormField([FromBody]SaveClientFormFieldRequest request)
        {
            var response = new CommonResponse();
            try
            {
                var isNew = request.ClientFormField.Id == -1;
                response = bussinnessLayer.SaveClientFormField(request.ClientFormField);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("removeClientFormField/{idClientForm}/{idFormField}")]
        public CommonResponse RemoveClientFormField(long idClientForm, long idFormField)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.RemoveClientFormField(idClientForm, idFormField);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpPost("addClientFormField")]
        public CommonResponse AddClientFormField([FromBody]AddClientFormFieldRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.AddClientFormField(request.IdClientForm, request.Description, request.Name, request.Placeholder, request.DataType, request.Constraints);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }
    }
}