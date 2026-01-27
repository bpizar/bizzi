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
    public class ClientFormController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ClientFormController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IDatabaseService ds)
        {
            _env = env;
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallclientForms")]
        public GetAllClientFormsResponse GetAllClientForms()
        {
            var response = new GetAllClientFormsResponse();

            try
            {
                var clientFormsAux = new List<ClientFormsCustomEntity>();
                bussinnessLayer.GetAllClientForms(out clientFormsAux);
                response.ClientForms = clientFormsAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getclientformsforeditbyid/{id}")]
        public GetClientFormResponse GetClientFormForEditById(long id)
        {
            var response = new GetClientFormResponse();

            try
            {
                var clientFormAux = new ClientFormsCustomEntity();
                var rolesAux = new List<Identity_RolesCustom>();

                bussinnessLayer.GetClientFormbyId(id, out clientFormAux);

                response.ClientForm = clientFormAux;
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("deleteClientForm/{id}")]
        public CommonResponse DeleteClientForm(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteClientForm(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveclientform")]
        public CommonResponse SaveClientForm([FromBody]SaveClientFormRequest request)
        {
            var response = new CommonResponse();
            try
            {

                if (request.ClientForm.TemplateFile == null)
                {
                    response = bussinnessLayer.SaveClientFormWithReminders(request.ClientForm, request.ClientFormReminders, request.FormFields);
                    response.Result = true;
                }
                else
                {
                    var webRoot = string.Format("{0}/media/files/{1}", _env.WebRootPath, "clientFormTemplates");
                    var generateName = Guid.NewGuid().ToString();
                    string convert = request.ClientForm.TemplateFile;/*.Replace("data:image/png;base64,", String.Empty);*/
                    string filetype = convert.Split(";")[0].Split("/")[1];
                    //switch (filetype)
                    //{
                    //    case "data:image/png":
                    //        generateName += ".png";
                    //        break;

                    //}
                    generateName += "." +filetype;
                    var path = string.Format("{0}/{1}", webRoot, generateName);
                    byte[] file = Convert.FromBase64String(convert.Split(";")[1].Replace("base64,", String.Empty));
                    if (file == null) throw new Exception("File is null");
                    if (file.Length == 0) throw new Exception("File is empty");
                    System.IO.File.WriteAllBytes(path, file);
                    request.ClientForm.TemplateFile = generateName;
                    response = bussinnessLayer.SaveClientFormWithReminders(request.ClientForm, request.ClientFormReminders, request.FormFields);
                    response.Result = true;
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallclientFormsByClient/{idClient}")]
        public GetAllClientFormsbyClientResponse GetAllClientFormsByClient(long idClient)
        {
            var response = new GetAllClientFormsbyClientResponse();
            
            try
            {
                var clientFormsbyClientAux = new List<ClientFormbyClientCustomEntity>();
                bussinnessLayer.GetAllClientFormByClients(idClient, out clientFormsbyClientAux);
                response.ClientFormsbyClient = clientFormsbyClientAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallclientFormsByClientandClientForm/{idClient}/{idClientForm}")]
        public GetAllClientFormsbyClientResponse GetAllClientFormsByClientandClientForm(long idClient,long idClientForm)
        {
            var response = new GetAllClientFormsbyClientResponse();

            try
            {
                var clientFormsbyClientAux = new List<ClientFormbyClientCustomEntity>();
                bussinnessLayer.GetAllClientFormsByClientandClientForm(idClient, idClientForm, out clientFormsbyClientAux);
                response.ClientFormsbyClient = clientFormsbyClientAux;
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