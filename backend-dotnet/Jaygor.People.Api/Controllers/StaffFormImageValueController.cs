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
    public class StaffFormImageValueController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public StaffFormImageValueController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IDatabaseService ds)
        {
            _env = env;
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallstaffFormImageValues")]
        public GetAllStaffFormImageValuesResponse GetAllStaffFormImageValues()
        {
            var response = new GetAllStaffFormImageValuesResponse();
            try
            {
                var staffFormImageValuesAux = new List<StaffFormImageValuesCustomEntity>();
                bussinnessLayer.GetAllStaffFormImageValues(out staffFormImageValuesAux);
                response.StaffFormImageValues = staffFormImageValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getstaffformImageValuesforeditbyid/{id}")]
        public GetStaffFormImageValueResponse GetStaffFormImageValueForEditById(long id)
        {
            var response = new GetStaffFormImageValueResponse();
            try
            {
                var staffFormImageValueAux = new StaffFormImageValuesCustomEntity();
                bussinnessLayer.GetStaffFormImageValuebyId(id, out staffFormImageValueAux);
                response.StaffFormImageValue = staffFormImageValueAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpPost("saveStaffFormImageValue")]
        public async Task<CommonResponse> SaveStaffFormImageValue([FromBody] SaveStaffFormImageValueRequest request)
        {
            var response = new CommonResponse();
            try
            {
                var generateName = Guid.NewGuid().ToString();
                var webRoot = string.Format("{0}/media/images/{1}", _env.WebRootPath,"staffFormValues");
                var path = string.Format("{0}/{1}.png", webRoot, generateName);
                if (request.StaffFormImageValue.Image == null)
                {
                    if (request.StaffFormImageValue.Id <= 0)
                        throw new Exception("File is null");
                    else
                    {
                        request.StaffFormImageValue.DateTime = DateTime.Now;
                        response = await bussinnessLayer.SaveStaffFormImageValue(request.StaffFormImageValue);
                        response.Result = true;
                    }
                }
                else
                {
                    string convert = request.StaffFormImageValue.Image.Replace("data:image/png;base64,", String.Empty);
                    byte[] file = Convert.FromBase64String(convert);
                    if (file == null) throw new Exception("File is empty");
                    if (file.Length == 0) throw new Exception("File is empty");
                    System.IO.File.WriteAllBytes(path, file);
                    request.StaffFormImageValue.Image = generateName;
                    request.StaffFormImageValue.DateTime = DateTime.Now;
                    response = await bussinnessLayer.SaveStaffFormImageValue(request.StaffFormImageValue);
                    response.Result = true;
                }    
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteStaffFormImageValue/{id}")]
        public CommonResponse DeleteStaffFormImageValue(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteStaffFormImageValue(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getStaffFormImageValueByStaffFormAndStaff/{idStaffForm}/{idStaff}")]
        public GetStaffFormImageValueResponse GetStaffFormImageValueByStaffFormAndStaff(long idStaffForm, long idStaff)
        {
            var response = new GetStaffFormImageValueResponse();
            try
            {
                var staffFormImageValuesAux = new StaffFormImageValuesCustomEntity();
                bussinnessLayer.GetStaffFormImageValueByStaffFormAndStaff(idStaffForm, idStaff, out staffFormImageValuesAux);
                response.StaffFormImageValue = staffFormImageValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getStaffFormImageValueImageByStaffFormAndStaff/{idStaffForm}/{idStaff}")]
        public string GetStaffFormImageValueImageByStaffFormAndStaff(long idStaffForm, long idStaff)
        {
            var staffFormImageValuesAux = GetStaffFormImageValueByStaffFormAndStaff(idStaffForm,idStaff);

            string fileName = staffFormImageValuesAux.StaffFormImageValue.Image+ ".png";
            //string path = _hostingEnvironment.WebRootPath + "/media/images/" + fileName;
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\media\images\" + "staffFormValues" + @"\" + fileName}";
            //string path = _hostingEnvironment.WebRootPath + "/media/images/" + fileName;
            //if (File.Exists(path))
            //{
            byte[] b = System.IO.File.ReadAllBytes(path);
            string jsonString;
            jsonString = JsonSerializer.Serialize("data:image/png;base64," + Convert.ToBase64String(b));
            return jsonString;
            //}
            //else
            //{
            //	return null;
            //}	
        }

        [HttpGet("getStaffFormImageValueImageById/{idStaffFormValue}")]
        public string GetStaffFormImageValueImageById(long idStaffFormValue)
        {
            var staffFormImageValuesAux = GetStaffFormImageValueForEditById(idStaffFormValue);
            string fileName = staffFormImageValuesAux.StaffFormImageValue.Image + ".png";
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\media\images\" + "staffFormValues" + @"\" + fileName}";
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