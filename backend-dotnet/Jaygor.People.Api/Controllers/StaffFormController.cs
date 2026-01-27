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
    public class StaffFormController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public StaffFormController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IDatabaseService ds)
        {
            _env = env;
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallstaffForms")]
        public GetAllStaffFormsResponse GetAllStaffForms()
        {
            var response = new GetAllStaffFormsResponse();

            try
            {
                var staffFormsAux = new List<StaffFormsCustomEntity>();
                bussinnessLayer.GetAllStaffForms(out staffFormsAux);
                response.StaffForms = staffFormsAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getstaffformsforeditbyid/{id}")]
        public GetStaffFormResponse GetStaffFormForEditById(long id)
        {
            var response = new GetStaffFormResponse();

            try
            {
                var staffFormAux = new StaffFormsCustomEntity();
                var rolesAux = new List<Identity_RolesCustom>();

                bussinnessLayer.GetStaffFormbyId(id, out staffFormAux);

                response.StaffForm = staffFormAux;
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("deleteStaffForm/{id}")]
        public CommonResponse DeleteStaffForm(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteStaffForm(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("savestaffform")]
        public CommonResponse SaveStaffForm([FromBody]SaveStaffFormRequest request)
        {
            var response = new CommonResponse();
            try
            {
                if (request.StaffForm.TemplateFile == null)
                {
                    response = bussinnessLayer.SaveStaffFormWithReminders(request.StaffForm, request.StaffFormReminders, request.FormFields);
                    response.Result = true;
                }
                else
                {
                    var webRoot = string.Format("{0}/media/files/{1}", _env.WebRootPath, "staffFormTemplates");
                    var generateName = Guid.NewGuid().ToString();
                    string convert = request.StaffForm.TemplateFile;
                    string filetype = convert.Split(";")[0].Split("/")[1];
                    generateName += "." + filetype;
                    var path = string.Format("{0}/{1}", webRoot, generateName);
                    byte[] file = Convert.FromBase64String(convert.Split(";")[1].Replace("base64,", String.Empty));
                    if (file == null) throw new Exception("File is null");
                    if (file.Length == 0) throw new Exception("File is empty");
                    System.IO.File.WriteAllBytes(path, file);
                    request.StaffForm.TemplateFile = generateName;
                    response = bussinnessLayer.SaveStaffFormWithReminders(request.StaffForm, request.StaffFormReminders, request.FormFields);
                    response.Result = true;
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallstaffFormsByStaff/{idStaff}")]
        public GetAllStaffFormsbyStaffResponse GetAllStaffFormsByStaff(long idStaff)
        {
            var response = new GetAllStaffFormsbyStaffResponse();
            
            try
            {
                var staffFormsbyStaffAux = new List<StaffFormbyStaffCustomEntity>();
                bussinnessLayer.GetAllStaffFormsByStaff(idStaff, out staffFormsbyStaffAux);
                response.StaffFormsbyStaff = staffFormsbyStaffAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallstaffFormsByStaffandStaffForm/{idStaff}/{idStaffForm}")]
        public GetAllStaffFormsbyStaffResponse GetAllStaffFormsByStaffandStaffForm(long idStaff,long idStaffForm)
        {
            var response = new GetAllStaffFormsbyStaffResponse();

            try
            {
                var staffFormsbyStaffAux = new List<StaffFormbyStaffCustomEntity>();
                bussinnessLayer.GetAllStaffFormsByStaffandStaffForm(idStaff, idStaffForm, out staffFormsbyStaffAux);
                response.StaffFormsbyStaff = staffFormsbyStaffAux;
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