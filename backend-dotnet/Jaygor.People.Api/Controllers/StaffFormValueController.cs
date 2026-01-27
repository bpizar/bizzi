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
    public class StaffFormValueController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public StaffFormValueController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallstaffFormValues")]
        public GetAllStaffFormValuesResponse GetAllStaffFormValues()
        {
            var response = new GetAllStaffFormValuesResponse();
            try
            {
                var staffFormValuesAux = new List<StaffFormValuesCustomEntity>();
                bussinnessLayer.GetAllStaffFormValues(out staffFormValuesAux);
                response.StaffFormValues = staffFormValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getstaffformValuesforeditbyid/{id}")]
        public GetStaffFormValueResponse GetStaffFormValueForEditById(long id)
        {
            var response = new GetStaffFormValueResponse();
            try
            {
                var staffFormValueAux = new StaffFormValuesCustomEntity();
                bussinnessLayer.GetStaffFormValuebyId(id, out staffFormValueAux);
                response.StaffFormValue = staffFormValueAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpDelete("deleteStaffFormValue/{id}")]
        public CommonResponse DeleteStaffFormValue(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteStaffFormValue(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("saveStaffFormValue")]
        public CommonResponse SaveStaffFormValue([FromBody]SaveStaffFormValueRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.SaveStaffFormValueWithDetail(request.StaffFormValue,request.StaffFormFieldValues);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpGet("getallstaffFormValuesByStaffFormAndStaff/{idStaffForm}/{idStaff}")]
        public GetAllStaffFormValuesResponse GetAllStaffFormValues(long idStaffForm, long idStaff)
        {
            var response = new GetAllStaffFormValuesResponse();
            try
            {
                var staffFormValuesAux = new List<StaffFormValuesCustomEntity>();
                bussinnessLayer.GetAllStaffFormValuesByStaffFormAndStaff(idStaffForm, idStaff, out staffFormValuesAux);
                response.StaffFormValues = staffFormValuesAux;
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