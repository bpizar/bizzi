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
    public class StaffFormFieldValueController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public StaffFormFieldValueController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallstaffFormFieldValues")]
        public GetAllStaffFormFieldValuesResponse GetAllStaffFormFieldValues()
        {
            var response = new GetAllStaffFormFieldValuesResponse();

            try
            {
                var staffFormFieldValuesAux = new List<StaffFormFieldValuesCustomEntity>();
                bussinnessLayer.GetAllStaffFormFieldValues(out staffFormFieldValuesAux);
                response.StaffFormFieldValues = staffFormFieldValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getstaffformfieldValuesforeditbyid/{id}")]
        public GetStaffFormFieldValueResponse GetStaffFormFieldValueForEditById(long id)
        {
            var response = new GetStaffFormFieldValueResponse();
            try
            {
                var staffFormFieldValueAux = new StaffFormFieldValuesCustomEntity();
                bussinnessLayer.GetStaffFormFieldValuebyId(id, out staffFormFieldValueAux);
                response.StaffFormFieldValue = staffFormFieldValueAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("deleteStaffFormFieldValue/{id}")]
        public CommonResponse DeleteStaffFormFieldValue(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteStaffFormFieldValue(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("savestaffformfieldvalue")]
        public CommonResponse SaveStaffFormFieldValue([FromBody]SaveStaffFormFieldValueRequest request)
        {
            var response = new CommonResponse();
            try
            {
                var isNew = request.StaffFormFieldValue.Id == -1;
                response = bussinnessLayer.SaveStaffFormFieldValue(request.StaffFormFieldValue);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getallstaffFormFieldValuesByStaffFormAndStaff/{idStaffForm}/{idStaff}")]
        public GetAllStaffFormFieldValuesResponse GetAllStaffFormFieldValues(long idStaffForm, long idStaff)
        {
            var response = new GetAllStaffFormFieldValuesResponse();
            try
            {
                var staffFormFieldValuesAux = new List<StaffFormFieldValuesCustomEntity>();
                bussinnessLayer.GetAllStaffFormFieldValuesByStaffFormAndStaff(idStaffForm, idStaff, out staffFormFieldValuesAux);
                response.StaffFormFieldValues = staffFormFieldValuesAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }
        
        [HttpGet("getallstaffFormFieldValuesByStaffFormValue/{idStaffFormValue}")]
        public GetAllStaffFormFieldValuesResponse GetAllStaffFormFieldValues(long idStaffFormValue)
        {
            var response = new GetAllStaffFormFieldValuesResponse();
            try
            {
                var staffFormFieldValuesAux = new List<StaffFormFieldValuesCustomEntity>();
                bussinnessLayer.GetAllStaffFormFieldValuesByStaffFormValue(idStaffFormValue, out staffFormFieldValuesAux);
                response.StaffFormFieldValues = staffFormFieldValuesAux;
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