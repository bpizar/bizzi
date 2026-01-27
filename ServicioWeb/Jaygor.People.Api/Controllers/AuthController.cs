using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using JayGor.People.Bussinness;
using JayGor.People.Api.auth;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities;
using System;
using JayGor.People.ErrorManager;
using JayGor.People.Entities.Entities;
using JayGor.People.Api.helpers;
// using Microsoft.Extensions.Logging;
using System.Linq;
using JayGor.People.Entities.Responses;
using JayGor.People.DataAccess;
using QRCoder;
using System.Drawing;

namespace Jaygor.People.Api.Controllers
{
    [Route("[controller]")]
    public class AuthController : Microsoft.AspNetCore.Mvc.Controller
    {
        // nuevo
        // private readonly ILogger<AuthController> _logger;

        readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();
        readonly IJwtFactory _jwtFactory;
        readonly JwtIssuerOptions _jwtOptions;
		//private readonly BussinnessLayer bussinnessLayer = new BussinnessLayer();
		

        //,ILogger<AuthController> logger
        public AuthController(IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IDatabaseService ds)
		{
			_jwtFactory = jwtFactory;
			_jwtOptions = jwtOptions.Value;
            // _logger = logger;
            this.bussinnessLayer = new BussinnessLayer(ds);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Post([FromBody]Credentials credentials)
		{
		    // _logger.LogInformation("Calling login controller");

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);

            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }
            else
            {
                var client = bussinnessLayer.IdentityGetUserByCredentials(credentials.UserName, credentials.Password);

                if (client != null)
                { 
                    bussinnessLayer.IdentityUpdateIdOneSignalBrowser(client.Id, credentials.OneSignalId);
                }
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
    		
            return new OkObjectResult(jwt);			
		}

        [HttpGet("loginmobile/{UserName}/{Password}/{IdOneSignal}/{facestamp}")]
        //public async Task<IActionResult> LoginMobile([FromBody]CredentialsMobile credentials) // string username, string
        public async Task<IActionResult> LoginMobile(string UserName, string Password,string IdOneSignal, string facestamp)
		{
			// _logger.LogInformation("Calling login controller");
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var identity = await GetClaimsIdentity(UserName, Password);

			if (identity == null)
			{
				return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
			}

            var client = bussinnessLayer.IdentityGetUserByCredentials(UserName, Password);

            if(!bussinnessLayer.IdentityUpdateIdOneSignal(client.Id, IdOneSignal))
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "OneSignal identifier was not udpated", ModelState));
            }

            var jwt = await Tokens.GenerateJwtMobile(identity,
                                                     _jwtFactory,
                                                     UserName,
                                                     _jwtOptions,
                                                     new JsonSerializerSettings { Formatting = Formatting.Indented },
                                                     //client.Id,
                                                     client.FirstName,
                                                     client.LastName,
                                                     "", //facestamp != client.FaceStamp ? client.Face : "",
                                                     client.identity_users_rol.FirstOrDefault(c => c.IdfRolNavigation.Rol == "faceRecorder" && c.State != "D") != null,
                                                     client.GeoTrackingEvery,
                                                     client.FaceStamp);
			

            return new OkObjectResult(jwt);
		}

		private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
				return await Task.FromResult<ClaimsIdentity>(null);

            identity_users userToVerify = null;  

            try
            {
                userToVerify = this.bussinnessLayer.IdentityGetUserByCredentials(userName, password);
			}
            catch (Exception ex)
            {
				//_logger.LogError(ex.Message);


				//var gmailhelper = new HelperEmail();
				//var emailName = string.Format("{0} {1}", "Error", "jaygor ");
				//var emailTo = "napilex@gmail.com";
                // var messageTo = string.Format("{0} {1}", ex.Message, ex.InnerException !=null ? ex.InnerException.Message : " inner vacio");
				//gmailhelper.SendGmailEmailAsync(emailName, emailTo, "Jaygor Password Reset", messageTo);


                bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description);
            }


            var listClaimRoles = new List<Claim>();

            if(userToVerify!=null)
            {
				foreach (var r in userToVerify.identity_users_rol)
				{
					listClaimRoles.Add(new Claim(ClaimTypes.Role, r.IdfRolNavigation.Rol));
				}

				return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id.ToString(),listClaimRoles));
            }

			return await Task.FromResult<ClaimsIdentity>(null);
		}

        [HttpGet("verifyTFAToken/{TFAToken}")]
        public async Task<IActionResult> verifyTFAToken(string TFAToken)
        {
            var userRequesting = HttpContext.User.Claims.Single(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            string TFAValue = bussinnessLayer.GetTFACode(bussinnessLayer.IdentityGetUserByEmail(userRequesting).TFASecret);
            return new OkObjectResult(TFAToken == TFAValue);
        }

        [HttpGet("generateTFASecret")]
        public IActionResult GenerateTFAToken()
        {
            var userRequesting = HttpContext.User.Claims.Single(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            identity_users user = bussinnessLayer.IdentityGetUserByEmail(userRequesting);
            string email = user.Email;
            string secret = user.TFASecret = bussinnessLayer.GenerateTFASecret(64);
            bussinnessLayer.IdentityUpdateTFASecret(user.Id, secret);
            string qrText = @"otpauth://totp/Bizzi:" + email + "?secret=" + secret + "&issuer=Bizzi";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText,
            QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            using (var stream = new System.IO.MemoryStream())
            {
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return File(stream.ToArray(), "image/jpeg");
            }
        }

        ////[AllowAnonymous]
        //[Authorize(Roles = "admin")]
        //[HttpPost("createuser")]
        //public CommonResponse CreateUser(string email, string password)
        //{
        //    var response = new CommonResponse();

        //    try
        //    {
        //        response = negocio.IdentityCreateUser(email, password);
        //    }
        //    catch (Exception ex)
        //    {
        //        //var response2 = negocio.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description);
        //        //response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(response2.TagInfo));
        //    }
            
        //    return response;
        //}
    }
}