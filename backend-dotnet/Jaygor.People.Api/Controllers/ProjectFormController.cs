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
    public class ProjectFormController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ProjectFormController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IDatabaseService ds)
        {
            _env = env;
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallprojectForms")]
        public GetAllProjectFormsResponse GetAllProjectForms()
        {
            var response = new GetAllProjectFormsResponse();

            try
            {
                var projectFormsAux = new List<ProjectFormsCustomEntity>();
                bussinnessLayer.GetAllProjectForms(out projectFormsAux);
                response.ProjectForms = projectFormsAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getprojectformsforeditbyid/{id}")]
        public GetProjectFormResponse GetProjectFormForEditById(long id)
        {
            var response = new GetProjectFormResponse();

            try
            {
                var projectFormAux = new ProjectFormsCustomEntity();
                var rolesAux = new List<Identity_RolesCustom>();

                bussinnessLayer.GetProjectFormbyId(id, out projectFormAux);

                response.ProjectForm = projectFormAux;
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("deleteProjectForm/{id}")]
        public CommonResponse DeleteProjectForm(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteProjectForm(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveprojectform")]
        public CommonResponse SaveProjectForm([FromBody]SaveProjectFormRequest request)
        {
            var response = new CommonResponse();
            try
            {
                if (request.ProjectForm.TemplateFile == null)
                {
                    response = bussinnessLayer.SaveProjectFormWithReminders(request.ProjectForm, request.ProjectFormReminders, request.FormFields);
                    response.Result = true;
                }
                else
                {
                    var webRoot = string.Format("{0}/media/files/{1}", _env.WebRootPath, "projectFormTemplates");
                    var generateName = Guid.NewGuid().ToString();
                    string convert = request.ProjectForm.TemplateFile;
                    string filetype = convert.Split(";")[0].Split("/")[1];
                    generateName += "." + filetype;
                    var path = string.Format("{0}/{1}", webRoot, generateName);
                    byte[] file = Convert.FromBase64String(convert.Split(";")[1].Replace("base64,", String.Empty));
                    if (file == null) throw new Exception("File is null");
                    if (file.Length == 0) throw new Exception("File is empty");
                    System.IO.File.WriteAllBytes(path, file);
                    request.ProjectForm.TemplateFile = generateName;
                    response = bussinnessLayer.SaveProjectFormWithReminders(request.ProjectForm, request.ProjectFormReminders, request.FormFields);
                    response.Result = true;
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallprojectFormsByProject/{idProject}")]
        public GetAllProjectFormsbyProjectResponse GetAllProjectFormsByProject(long idProject)
        {
            var response = new GetAllProjectFormsbyProjectResponse();
            
            try
            {
                var projectFormsbyProjectAux = new List<ProjectFormbyProjectCustomEntity>();
                bussinnessLayer.GetAllProjectFormByProjects(idProject, out projectFormsbyProjectAux);
                response.ProjectFormsbyProject = projectFormsbyProjectAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallprojectFormsByProjectandProjectForm/{idProject}/{idProjectForm}")]
        public GetAllProjectFormsbyProjectResponse GetAllProjectFormsByProjectandProjectForm(long idProject, long idProjectForm)
        {
            var response = new GetAllProjectFormsbyProjectResponse();

            try
            {
                var projectFormsbyProjectAux = new List<ProjectFormbyProjectCustomEntity>();
                bussinnessLayer.GetAllProjectFormsByProjectandProjectForm(idProject, idProjectForm, out projectFormsbyProjectAux);
                response.ProjectFormsbyProject = projectFormsbyProjectAux;
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