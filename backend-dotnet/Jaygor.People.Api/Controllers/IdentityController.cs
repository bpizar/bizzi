using System;
using Microsoft.AspNetCore.Mvc;
using JayGor.People.Bussinness;
using Microsoft.AspNetCore.Authorization;
using JayGor.People.ErrorManager;
using JayGor.People.Entities.Responses;
using JayGor.People.Api.helpers;
using System.Linq;
using JayGor.People.DataAccess;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class IdentityController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public IdentityController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }


        [Authorize(Roles = "user")]
        [HttpPost("changemypassword")]
        public CommonResponse ChangeMyPassword([FromBody]ChangeMyPasswordRequest request)
        {
            var response = new CommonResponse();
            // var userRequesting = HttpContext.User;
            var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;

            try
            {
                response = bussinnessLayer.ChangeMyPassword(userRequesting,
                                                            request.CurrentPassword,
                                                            request.NewPassword,
                                                            request.ConfirmNewPassword);

                if (response.Result)
                {
                    var user = bussinnessLayer.IdentityGetUserByEmail(userRequesting);
                    // var gmailhelper = new HelperEmail();
                    var emailName = string.Format("{0} {1}", user.LastName, user.FirstName);
                    var emailTo = user.Email;
                    var messageTo = string.Format("Hi {0}, Your changed succefuly the password for you account {1} on date {2}, your new password is {3}", emailName, emailTo, DateTime.Now.ToLocalTime(), request.NewPassword);
                    HelperEmail.SendEmailAsync(emailName, emailTo, "Jaygor Password Reset", messageTo);
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


        [Authorize(Roles = "user")]
        [HttpGet("getmyaccount")]
        public GetMyAccountResponse GetMyAccount()
        {
            var response = new GetMyAccountResponse();
            //var userRequesting = HttpContext.User;
            var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;

            try
            {
                response.User = bussinnessLayer.GetMyAccount(userRequesting); // idetity name
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("createuser")]
        public CommonResponse CreateUser(string email, string password)
        {
            var response = new CommonResponse();

            try
            {
                response = bussinnessLayer.IdentityCreateUser(email, password);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            
            return response;
        }

        [HttpGet("savewebonesignalid/{idonesignal}")]
        public CommonResponse SaveWebOneSignalId(string idonesignal)
        {
            var response = new CommonResponse();
            var userRequesting = HttpContext.User.Claims.Single(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                var client = bussinnessLayer.IdentityGetUserByEmail(userRequesting);
                response.Result = bussinnessLayer.IdentityUpdateIdOneSignalBrowser(client.Id, idonesignal);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }
    
    }
}
