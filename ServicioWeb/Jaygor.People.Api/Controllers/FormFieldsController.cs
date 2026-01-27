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
    public class FormFieldController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public FormFieldController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallformfields")]
        public GetAllFormFieldsResponse GetAllFormFields()
        {
            var response = new GetAllFormFieldsResponse();

            try
            {
                var formFieldAux = new List<FormFieldsCustomEntity>();
                bussinnessLayer.GetAllFormFields(out formFieldAux);
                response.FormFields = formFieldAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getformfieldforeditbyid/{id}")]
        public GetFormFieldResponse GetFormFieldForEditById(long id)
        {
            var response = new GetFormFieldResponse();

            try
            {
                var formFieldAux = new FormFieldsCustomEntity();
                var rolesAux = new List<Identity_RolesCustom>();

                bussinnessLayer.GetFormFieldsbyId(id, out formFieldAux);

                response.FormField = formFieldAux;
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("deleteFormField/{id}")]
        public CommonResponse DeleteFormField(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteFormField(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveformfield")]
        public CommonResponse SaveFormField([FromBody]SaveFormFieldRequest request)
        {
            var response = new CommonResponse();
            try
            {
                var isNew = request.FormField.Id == -1;
                response = bussinnessLayer.SaveFormField(request.FormField);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallclientFormFieldsbyclientform/{id}")]
        public GetAllFormFieldsResponse GetAllFormFieldsByClientForm(long id)
        {
            var response = new GetAllFormFieldsResponse();

            try
            {
                var formFieldsByClientAux = new List<FormFieldsCustomEntity>();
                bussinnessLayer.GetAllFormFieldsByClientForm(id, out formFieldsByClientAux);
                response.FormFields = formFieldsByClientAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallprojectFormFieldsbyprojectform/{id}")]
        public GetAllFormFieldsResponse GetAllFormFieldsByProjectForm(long id)
        {
            var response = new GetAllFormFieldsResponse();

            try
            {
                var formFieldsByProjectAux = new List<FormFieldsCustomEntity>();
                bussinnessLayer.GetAllFormFieldsByProjectForm(id, out formFieldsByProjectAux);
                response.FormFields = formFieldsByProjectAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallstaffFormFieldsbystaffform/{id}")]
        public GetAllFormFieldsResponse GetAllFormFieldsByStaffForm(long id)
        {
            var response = new GetAllFormFieldsResponse();

            try
            {
                var formFieldsByStaffAux = new List<FormFieldsCustomEntity>();
                bussinnessLayer.GetAllFormFieldsByStaffForm(id, out formFieldsByStaffAux);
                response.FormFields = formFieldsByStaffAux;
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