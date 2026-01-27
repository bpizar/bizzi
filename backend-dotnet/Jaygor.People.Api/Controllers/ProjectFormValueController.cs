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
    public class ProjectFormValueController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ProjectFormValueController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallprojectFormValues")]
        public GetAllProjectFormValuesResponse GetAllProjectFormValues()
        {
            var response = new GetAllProjectFormValuesResponse();
            try
            {
                var projectFormValuesAux = new List<ProjectFormValuesCustomEntity>();
                bussinnessLayer.GetAllProjectFormValues(out projectFormValuesAux);
                response.ProjectFormValues = projectFormValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getprojectformValuesforeditbyid/{id}")]
        public GetProjectFormValueResponse GetProjectFormValueForEditById(long id)
        {
            var response = new GetProjectFormValueResponse();
            try
            {
                var projectFormValueAux = new ProjectFormValuesCustomEntity();
                bussinnessLayer.GetProjectFormValuebyId(id, out projectFormValueAux);
                response.ProjectFormValue = projectFormValueAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteProjectFormValue/{id}")]
        public CommonResponse DeleteProjectFormValue(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteProjectFormValue(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveProjectFormValue")]
        public CommonResponse SaveProjectFormValue([FromBody]SaveProjectFormValueRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.SaveProjectFormValueWithDetail(request.ProjectFormValue,request.ProjectFormFieldValues);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getallprojectFormValuesByProjectFormAndProject/{idProjectForm}/{idProject}")]
        public GetAllProjectFormValuesResponse GetAllProjectFormValues(long idProjectForm, long idProject)
        {
            var response = new GetAllProjectFormValuesResponse();
            try
            {
                var projectFormValuesAux = new List<ProjectFormValuesCustomEntity>();
                bussinnessLayer.GetAllProjectFormValuesByProjectFormAndProject(idProjectForm, idProject, out projectFormValuesAux);
                response.ProjectFormValues = projectFormValuesAux;
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