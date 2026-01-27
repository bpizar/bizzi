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
    public class StaffController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;




        public StaffController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }
        //private readonly ILogger _logger;

        //private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        // , ILogger<StaffController> logger
        // Microsoft.AspNetCore.Hosting.IHostingEnvironment env

        // public StaffController()
        // {
        //     //_env = env;
        //     //  _logger = logger;
        // }

        [HttpGet("getallstaffs")]
        public GetAllStaffResponse GetAllStaffs()
        {
            var response = new GetAllStaffResponse();

            try
            {
                var staffsAux = new List<StaffCustomEntity>();
                var positionsAux = new List<positions>();
                bussinnessLayer.GetAllStaff(out staffsAux, out positionsAux);
                response.Staffs = staffsAux;
                response.Positions = positionsAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getstaff")]
        public GetStaffsResponse GetStaffs()
        {
            var response = new GetStaffsResponse();

            try
            {
                response.Staffs = bussinnessLayer.GetStaffs();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getstafflist")]
        public GetStaffsListResponse GetStaffsList()
        {
            var response = new GetStaffsListResponse();

            try
            {
                bussinnessLayer.GetStaffs().ToList().ForEach(p => {
                    response.Staffs.Add(new StaffListCustomEntity()
                    {
                        Id = p.Id,
                        FullName = p.FullName,
                        PositionName = p.PositionName,
                        Email = p.Email,
                        IdfUser = p.IdfUser,
                        Img = p.Img,
                        CellNumber = p.CellNumber,
                        HomePhone = p.HomePhone,
                        City = p.City,
                        ProjectInfo = p.ProjectInfo
                    });
                });
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getstaffforeditbyid/{id}")]
        public GetStaffResponse GetStaffForEditById(long id)
        {
            var response = new GetStaffResponse();

            try
            {
                var staffAux = new StaffCustomEntity();
                var rolesAux = new List<Identity_RolesCustom>();

                bussinnessLayer.GetStaffbyId(id, out staffAux,out rolesAux);

                response.Staff = staffAux;
                response.Roles = rolesAux;
                response.Result = true;


                //if (id >= 0)
                //{
                //    staffAux = this.ds_.GetStaffbyId(id);
                //    staffAux.IdfUserNavigation.Password = "*";
                //    staffAux.IdfUserNavigation.Face = "";

                //    // fix issue redundancia ciclica en objetos.
                //    rolesAux = this.ds_.IdentityGetRoles(id).ToList().Select(x => new Identity_RolesCustom
                //    {
                //        Abm = x.Abm,
                //        DisplayShortName = x.DisplayShortName,
                //        Group = "",
                //        Id = x.Id,
                //        IsInrole = x.IsInrole,
                //        Rol = x.Rol,
                //        RolDescription = x.RolDescription,
                //        State = x.State
                //    }).ToList();
                //}


                //response.Staff = staffAux;
                //response.Roles.AddRange(rolesAux);
                //response.Result = true;


            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("resetpassword/{id}")]
        public CommonResponse ResetPassword(long id)
        {
            var response = new CommonResponse();

            try
            {                
                var password = new Random().Next(10000, 99000).ToString();

                if (bussinnessLayer.ResetPassword(id, password))
                {
                    var user = bussinnessLayer.IdentityGetUserById(id);
                    //var gmailhelper = new HelperEmail();
                    var emailName = string.Format("{0} {1}", user.LastName, user.FirstName);
                    var emailTo = user.Email;                    
                    var messageTo = string.Format("Hi {0}, The password for you account {1} was reset, your new password is {2}", emailName, emailTo, password);
					//gmailhelper.SendEmailAsync(emailName, emailTo, "Jaygor Password Reset", messageTo);
                    HelperEmail.Config();
                    HelperEmail.SendEmailAsync(emailName, emailTo, "Jaygor Password Reset", messageTo);
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        //[HttpPost]
        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("savestaff")]
        public CommonResponse SaveStaff([FromBody]SaveStaffRequest request)
        {
            var response = new CommonResponse();
            var password = string.Empty;

            try
            {
                var isNew = request.Staff.Id == -1;

                if (isNew)
                {
                    var user = bussinnessLayer.IdentityGetUserByEmail(request.User.Email);

                    if (user != null)
                    {
                        

                        response.Result = false;
                        response.Messages.Add(new GenericPair { Id = "10002", Description = "The account is in use" });
                        return response;
                    }
                }

                // var webRoot = string.Format("{0}/", _env.WebRootPath);
                // var location = "/media/images/users/";
                var workingHoursByPeriodStaff = Convert.ToInt32(CommonHelper.StaffWorkingHourDefault());

                if(isNew)
                {
					
                    password = new Random().Next(10000, 88000).ToString();
					response = bussinnessLayer.SaveStaff(request.Staff, request.User, request.Roles, string.Empty, string.Empty, password, null,workingHoursByPeriodStaff, request.IdfPeriod);
                    //var user = bussinnessLayer.IdentityGetUserByEmail(userRequesting);
					//var gmailhelper = new HelperEmail();
                    var emailName = string.Format("{0} {1}",request.User.LastName,request.User.FirstName);
					var emailTo = request.User.Email;
                    var messageTo = string.Format("Wellcome to People!!!, Your account {0} was created successfully your password is {1}", emailTo, password);
                    HelperEmail.SendEmailAsync(emailName, emailTo, "Wellcome to People!!! ", messageTo);    
                }
                else{
                    response = bussinnessLayer.SaveStaff(request.Staff, request.User, request.Roles, string.Empty, string.Empty, password,request.staffPeriodSettings,workingHoursByPeriodStaff, request.IdfPeriod);
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        // DEPRECAR ????
        [HttpGet("getstaffforplanning/{groupby}")]
        public GetStaffForPlanningResponse GetStaffForPlanning(string groupby)
        {
            var response = new GetStaffForPlanningResponse();

            try
            {
                response.Staffs = bussinnessLayer.GetStaffForPlanning(groupby);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

   //     [Authorize(Roles = "admin,projecteditor,user")]
   //     [HttpGet("getstaffforscheduling/{idperiod}")]
   //     public GetStaffForSchedulingResponse GetStaffForScheduling(long idperiod)
   //     {
   //         var response = new GetStaffForSchedulingResponse();
   //         //var userRequesting = HttpContext.User;
			//var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;


        //    var staffsAux = new List<StaffForPlanningCustomEntity>();
        //    var overtimeAux = new List<OverTimeCustomEntity>();
			
        //    try
        //    {
        //        if (HttpContext.User.IsInRole("admin"))
        //        {
        //            bussinnessLayer.GetStaffForScheduling(idperiod,out staffsAux, out overtimeAux);
        //        }
        //        else if (HttpContext.User.IsInRole("schedulingeditor"))
        //        {
        //            response.Staffs = bussinnessLayer.GetStaffByOwnProjects(idperiod, userRequesting).ToList();
        //        }
        //        else if (HttpContext.User.IsInRole("user"))
        //        {
        //            response.Staffs = bussinnessLayer.GetStaffByOwnScheduling(idperiod, userRequesting).ToList();
        //        }


        //        response.Staffs = staffsAux;
        //        response.OverTime = overtimeAux;
        //        response.Result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
        //    }

        //    return response;
        //}

        [HttpPost]
        [Route("enabledisableaccount")]
        public CommonResponse EnableDisableAccount([FromBody]EnableDisableAccountRequest request)
        {
            var response = new CommonResponse();

            try
            {
                response = bussinnessLayer.EnableDisableAccount(request.IdUser, request.State);
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