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
    public class ProjectFormFieldController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ProjectFormFieldController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallprojectFormFields")]
        public GetAllProjectFormFieldsResponse GetAllProjectFormFields()
        {
            var response = new GetAllProjectFormFieldsResponse();

            try
            {
                var projectFormFieldsAux = new List<ProjectFormFieldsCustomEntity>();
                bussinnessLayer.GetAllProjectFormFields(out projectFormFieldsAux);
                response.ProjectFormFields = projectFormFieldsAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getprojectformfieldsforeditbyid/{id}")]
        public GetProjectFormFieldResponse GetProjectFormFieldForEditById(long id)
        {
            var response = new GetProjectFormFieldResponse();

            try
            {
                var projectFormFieldAux = new ProjectFormFieldsCustomEntity();
                var rolesAux = new List<Identity_RolesCustom>();

                bussinnessLayer.GetProjectFormFieldbyId(id, out projectFormFieldAux);

                response.ProjectFormField = projectFormFieldAux;
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("deleteProjectFormField/{id}")]
        public CommonResponse DeleteProjectFormField(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteProjectFormField(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveprojectformfield")]
        public CommonResponse SaveProjectFormField([FromBody]SaveProjectFormFieldRequest request)
        {
            var response = new CommonResponse();
            try
            {
                var isNew = request.ProjectFormField.Id == -1;
                response = bussinnessLayer.SaveProjectFormField(request.ProjectFormField);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("removeProjectFormField/{idProjectForm}/{idFormField}")]
        public CommonResponse RemoveProjectFormField(long idProjectForm, long idFormField)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.RemoveProjectFormField(idProjectForm, idFormField);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpPost("addProjectFormField")]
        public CommonResponse AddProjectFormField([FromBody]AddProjectFormFieldRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.AddProjectFormField(request.IdProjectForm, request.Name, request.Description, request.Placeholder, request.DataType, request.Constraints);
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