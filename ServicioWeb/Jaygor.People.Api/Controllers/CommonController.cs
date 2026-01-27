using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JayGor.People.Entities.Responses;
using JayGor.People.Bussinness;
using Microsoft.AspNetCore.Authorization;
using JayGor.People.ErrorManager;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using JayGor.People.DataAccess;
using System.Text.Json;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class CommonController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        private readonly BussinnessLayer bussinnessLayer;

        public CommonController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IDatabaseService ds)
        {
            _env = env;
            this.bussinnessLayer = new BussinnessLayer(ds);
        }

      
        [HttpGet("ping")]
        public string Ping()
        {
            return bussinnessLayer.Ping();
        }

		//[HttpPost]
		//[Route("upload")]
		//public async Task<CommonResponse> Upload(IFormFile file,string location, long realfilename)
		//{
		//    var response = new CommonResponse();

		//    try
		//    {
		//        if (file == null) throw new Exception("File is null");
		//        if (file.Length == 0) throw new Exception("File is empty");

		//        using (Stream stream = file.OpenReadStream())
		//        {
		//            using (var binaryReader = new BinaryReader(stream))
		//            {
		//                var webRoot = string.Format("{0}/media/images/{1}", _env.WebRootPath, location);
		//                var fileContent = binaryReader.ReadBytes((int)file.Length);
		//                await bussinnessLayer.UploadFile(fileContent, webRoot, realfilename, file.ContentType,location);
		//                response.Result = true;
		//            }
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
		//    }

		//    return response;
		//}

		[HttpPost]
		[Route("upload")]
		public async Task<CommonResponse> Upload(string file64, string location, long realfilename)
		{
			var response = new CommonResponse();

			try
			{
				//byte[] base64Bytes =  Convert.FromBase64String(file64);
				//string base64String = Encoding.UTF8.GetString(base64Bytes, 0, base64Bytes.Length);

				string convert = file64.Replace("data:image/png;base64,", String.Empty);

				byte[] file = Convert.FromBase64String(convert);

                //byte[] file = Convert.FromBase64String(base64String); 
				if (file == null) throw new Exception("File is null");
				if (file.Length == 0) throw new Exception("File is empty");

				//using (Stream stream = file.OpenReadStream())
				//{
				//	using (var binaryReader = new BinaryReader(stream))
				//	{
						var webRoot = string.Format("{0}/media/images/{1}", _env.WebRootPath, location);
						//var fileContent = binaryReader.ReadBytes((int)file.Length);
						await bussinnessLayer.UploadFile(file, webRoot, realfilename, location);
						response.Result = true;
				//	}
				//}
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}
		[HttpGet("image/{location}/{fileName}")]
		public string Get(string location, string fileName)
		{
			fileName = "3eaefd9c-28e9-4eb5-bc95-213eab35545c"+".png";
			//string path = _hostingEnvironment.WebRootPath + "/media/images/" + fileName;
			string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\media\images\" + location + @"\" + fileName}";
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
	}
}