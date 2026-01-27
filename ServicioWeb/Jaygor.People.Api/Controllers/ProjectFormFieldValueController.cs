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
    public class ProjectFormFieldValueController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ProjectFormFieldValueController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallprojectFormFieldValues")]
        public GetAllProjectFormFieldValuesResponse GetAllProjectFormFieldValues()
        {
            var response = new GetAllProjectFormFieldValuesResponse();

            try
            {
                var projectFormFieldValuesAux = new List<ProjectFormFieldValuesCustomEntity>();
                bussinnessLayer.GetAllProjectFormFieldValues(out projectFormFieldValuesAux);
                response.ProjectFormFieldValues = projectFormFieldValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getprojectformfieldValuesforeditbyid/{id}")]
        public GetProjectFormFieldValueResponse GetProjectFormFieldValueForEditById(long id)
        {
            var response = new GetProjectFormFieldValueResponse();
            try
            {
                var projectFormFieldValueAux = new ProjectFormFieldValuesCustomEntity();
                bussinnessLayer.GetProjectFormFieldValuebyId(id, out projectFormFieldValueAux);
                response.ProjectFormFieldValue = projectFormFieldValueAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("deleteProjectFormFieldValue/{id}")]
        public CommonResponse DeleteProjectFormFieldValue(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteProjectFormFieldValue(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveprojectformfieldvalue")]
        public CommonResponse SaveProjectFormFieldValue([FromBody]SaveProjectFormFieldValueRequest request)
        {
            var response = new CommonResponse();
            try
            {
                var isNew = request.ProjectFormFieldValue.Id == -1;
                response = bussinnessLayer.SaveProjectFormFieldValue(request.ProjectFormFieldValue);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallprojectFormFieldValuesByProjectFormAndProject/{idProjectForm}/{idProject}")]
        public GetAllProjectFormFieldValuesResponse GetAllProjectFormFieldValues(long idProjectForm, long idProject)
        {
            var response = new GetAllProjectFormFieldValuesResponse();
            try
            {
                var projectFormFieldValuesAux = new List<ProjectFormFieldValuesCustomEntity>();
                bussinnessLayer.GetAllProjectFormFieldValuesByProjectFormAndProject(idProjectForm, idProject, out projectFormFieldValuesAux);
                response.ProjectFormFieldValues = projectFormFieldValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }
        
        [HttpGet("getallprojectFormFieldValuesByProjectFormValue/{idProjectFormValue}")]
        public GetAllProjectFormFieldValuesResponse GetAllProjectFormFieldValues(long idProjectFormValue)
        {
            var response = new GetAllProjectFormFieldValuesResponse();
            try
            {
                var projectFormFieldValuesAux = new List<ProjectFormFieldValuesCustomEntity>();
                bussinnessLayer.GetAllProjectFormFieldValuesByProjectFormValue(idProjectFormValue, out projectFormFieldValuesAux);
                response.ProjectFormFieldValues = projectFormFieldValuesAux;
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