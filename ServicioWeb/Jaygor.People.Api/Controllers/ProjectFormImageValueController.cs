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
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class ProjectFormImageValueController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ProjectFormImageValueController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IDatabaseService ds)
        {
            _env = env;
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallprojectFormImageValues")]
        public GetAllProjectFormImageValuesResponse GetAllProjectFormImageValues()
        {
            var response = new GetAllProjectFormImageValuesResponse();
            try
            {
                var projectFormImageValuesAux = new List<ProjectFormImageValuesCustomEntity>();
                bussinnessLayer.GetAllProjectFormImageValues(out projectFormImageValuesAux);
                response.ProjectFormImageValues = projectFormImageValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getprojectformImageValuesforeditbyid/{id}")]
        public GetProjectFormImageValueResponse GetProjectFormImageValueForEditById(long id)
        {
            var response = new GetProjectFormImageValueResponse();
            try
            {
                var projectFormImageValueAux = new ProjectFormImageValuesCustomEntity();
                bussinnessLayer.GetProjectFormImageValuebyId(id, out projectFormImageValueAux);
                response.ProjectFormImageValue = projectFormImageValueAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteProjectFormImageValue/{id}")]
        public CommonResponse DeleteProjectFormImageValue(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteProjectFormImageValue(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpPost("saveProjectFormImageValue")]
        public async Task<CommonResponse> SaveProjectFormImageValue([FromBody] SaveProjectFormImageValueRequest request)
        {
            var response = new CommonResponse();
            try
            {
                var generateName = Guid.NewGuid().ToString();
                var webRoot = string.Format("{0}/media/images/{1}", _env.WebRootPath, "projectFormValues");
                var path = string.Format("{0}/{1}.png", webRoot, generateName);
                if (request.ProjectFormImageValue.Image == null)
                {
                    if (request.ProjectFormImageValue.Id <= 0)
                        throw new Exception("File is null");
                    else
                    {
                        request.ProjectFormImageValue.DateTime = DateTime.Now;
                        response = await bussinnessLayer.SaveProjectFormImageValue(request.ProjectFormImageValue);
                        response.Result = true;
                    }
                }
                else
                {
                    string convert = request.ProjectFormImageValue.Image.Replace("data:image/png;base64,", String.Empty);
                    byte[] file = Convert.FromBase64String(convert);
                    if (file == null) throw new Exception("File is null");
                    if (file.Length == 0) throw new Exception("File is empty");
                    System.IO.File.WriteAllBytes(path, file);
                    request.ProjectFormImageValue.Image = generateName;
                    request.ProjectFormImageValue.DateTime = DateTime.Now;
                    response = await bussinnessLayer.SaveProjectFormImageValue(request.ProjectFormImageValue);
                    response.Result = true;
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getProjectFormImageValueByProjectFormAndProject/{idProjectForm}/{idProject}")]
        public GetProjectFormImageValueResponse GetProjectFormImageValueByProjectFormAndProject(long idProjectForm, long idProject)
        {
            var response = new GetProjectFormImageValueResponse();
            try
            {
                var projectFormImageValuesAux = new ProjectFormImageValuesCustomEntity();
                bussinnessLayer.GetProjectFormImageValueByProjectFormAndProject(idProjectForm, idProject, out projectFormImageValuesAux);
                response.ProjectFormImageValue = projectFormImageValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getProjectFormImageValueImageByProjectFormAndProject/{idProjectForm}/{idProject}")]
        public string GetProjectFormImageValueImageByProjectFormAndProject(long idProjectForm, long idProject)
        {
            var projectFormImageValuesAux = GetProjectFormImageValueByProjectFormAndProject(idProjectForm,idProject);

            string fileName = projectFormImageValuesAux.ProjectFormImageValue.Image+ ".png";
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\media\images\" + "projectFormValues" + @"\" + fileName}";
            if (System.IO.File.Exists(path))
            {
                byte[] b = System.IO.File.ReadAllBytes(path);
            string jsonString;
            jsonString = JsonSerializer.Serialize("data:image/png;base64," + Convert.ToBase64String(b));
            return jsonString;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("getProjectFormImageValueImageById/{idProjectFormValue}")]
        public string GetProjectFormImageValueImageById(long idProjectFormValue)
        {
            var projectFormImageValuesAux = GetProjectFormImageValueForEditById(idProjectFormValue);
            string fileName = projectFormImageValuesAux.ProjectFormImageValue.Image + ".png";
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\media\images\" + "projectFormValues" + @"\" + fileName}";
            if (System.IO.File.Exists(path))
            {
                byte[] b = System.IO.File.ReadAllBytes(path);
                string jsonString;
                jsonString = JsonSerializer.Serialize("data:image/png;base64," + Convert.ToBase64String(b));
                return jsonString;
            }
            else
            {
                return null;
            }
        }
    }
}