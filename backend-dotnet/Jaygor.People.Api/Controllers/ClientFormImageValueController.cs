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
    public class ClientFormImageValueController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public ClientFormImageValueController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IDatabaseService ds)
        {
            _env = env;
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallclientFormImageValues")]
        public GetAllClientFormImageValuesResponse GetAllClientFormImageValues()
        {
            var response = new GetAllClientFormImageValuesResponse();
            try
            {
                var clientFormImageValuesAux = new List<ClientFormImageValuesCustomEntity>();
                bussinnessLayer.GetAllClientFormImageValues(out clientFormImageValuesAux);
                response.ClientFormImageValues = clientFormImageValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getclientformImageValuesforeditbyid/{id}")]
        public GetClientFormImageValueResponse GetClientFormImageValueForEditById(long id)
        {
            var response = new GetClientFormImageValueResponse();
            try
            {
                var clientFormImageValueAux = new ClientFormImageValuesCustomEntity();
                bussinnessLayer.GetClientFormImageValuebyId(id, out clientFormImageValueAux);
                response.ClientFormImageValue = clientFormImageValueAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteClientFormImageValue/{id}")]
        public CommonResponse DeleteClientFormImageValue(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteClientFormImageValue(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpPost("saveClientFormImageValue")]
        public async Task<CommonResponse> SaveClientFormImageValue([FromBody] SaveClientFormImageValueRequest request)
        {
            var response = new CommonResponse();
            try
            {
                var generateName = Guid.NewGuid().ToString();
                var webRoot = string.Format("{0}/media/images/{1}", _env.WebRootPath, "clientFormValues");
                var path = string.Format("{0}/{1}.png", webRoot, generateName);
                if (request.ClientFormImageValue.Image == null)
                {
                    if (request.ClientFormImageValue.Id <= 0)
                        throw new Exception("File is null");
                    else
                    {
                        request.ClientFormImageValue.DateTime = DateTime.Now;
                        response = await bussinnessLayer.SaveClientFormImageValue(request.ClientFormImageValue);
                        response.Result = true;
                    }
                }
                else
                {
                    string convert = request.ClientFormImageValue.Image.Replace("data:image/png;base64,", String.Empty);
                    byte[] file = Convert.FromBase64String(convert);
                    if (file == null) throw new Exception("File is null");
                    if (file.Length == 0) throw new Exception("File is empty");
                    System.IO.File.WriteAllBytes(path, file);
                    request.ClientFormImageValue.Image = generateName;
                    request.ClientFormImageValue.DateTime = DateTime.Now;
                    response = await bussinnessLayer.SaveClientFormImageValue(request.ClientFormImageValue);
                    response.Result = true;
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getClientFormImageValueByClientFormAndClient/{idClientForm}/{idClient}")]
        public GetClientFormImageValueResponse GetClientFormImageValueByClientFormAndClient(long idClientForm, long idClient)
        {
            var response = new GetClientFormImageValueResponse();
            try
            {
                var clientFormImageValuesAux = new ClientFormImageValuesCustomEntity();
                bussinnessLayer.GetClientFormImageValueByClientFormAndClient(idClientForm, idClient, out clientFormImageValuesAux);
                response.ClientFormImageValue = clientFormImageValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }
        [HttpGet("getClientFormImageValueImageByClientFormAndClient/{idClientForm}/{idClient}")]
        public string GetClientFormImageValueImageByClientFormAndClient(long idClientForm, long idClient)
        {
            var clientFormImageValuesAux = GetClientFormImageValueByClientFormAndClient(idClientForm,idClient);

            string fileName = clientFormImageValuesAux.ClientFormImageValue.Image+ ".png";
            //string path = _hostingEnvironment.WebRootPath + "/media/images/" + fileName;
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\media\images\" + "clientFormValues" + @"\" + fileName}";
            //string path = _hostingEnvironment.WebRootPath + "/media/images/" + fileName;
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

        [HttpGet("getClientFormImageValueImageById/{idClientFormValue}")]
        public string GetClientFormImageValueImageById(long idClientFormValue)
        {
            var clientFormImageValuesAux = GetClientFormImageValueForEditById(idClientFormValue);
            string fileName = clientFormImageValuesAux.ClientFormImageValue.Image + ".png";
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\media\images\" + "clientFormValues" + @"\" + fileName}";
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