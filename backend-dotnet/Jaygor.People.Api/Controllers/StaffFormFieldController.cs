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
    public class StaffFormFieldController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        IDatabaseService ds_ ;

        public StaffFormFieldController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
            this.ds_ = ds;
        }

        [HttpGet("getallstaffFormFields")]
        public GetAllStaffFormFieldsResponse GetAllStaffFormFields()
        {
            var response = new GetAllStaffFormFieldsResponse();

            try
            {
                var staffFormFieldsAux = new List<StaffFormFieldsCustomEntity>();
                bussinnessLayer.GetAllStaffFormFields(out staffFormFieldsAux);
                response.StaffFormFields = staffFormFieldsAux;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getstaffformfieldsforeditbyid/{id}")]
        public GetStaffFormFieldResponse GetStaffFormFieldForEditById(long id)
        {
            var response = new GetStaffFormFieldResponse();

            try
            {
                var staffFormFieldAux = new StaffFormFieldsCustomEntity();
                var rolesAux = new List<Identity_RolesCustom>();

                bussinnessLayer.GetStaffFormFieldbyId(id, out staffFormFieldAux);

                response.StaffFormField = staffFormFieldAux;
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("deleteStaffFormField/{id}")]
        public CommonResponse DeleteStaffFormField(long id)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.DeleteStaffFormField(id);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [Authorize(Roles = "admin,schedulingeditor")]
        [HttpPost("savestaffformfield")]
        public CommonResponse SaveStaffFormField([FromBody]SaveStaffFormFieldRequest request)
        {
            var response = new CommonResponse();
            try
            {
                var isNew = request.StaffFormField.Id == -1;
                response = bussinnessLayer.SaveStaffFormField(request.StaffFormField);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpDelete("removeStaffFormField/{idStaffForm}/{idFormField}")]
        public CommonResponse RemoveStaffFormField(long idStaffForm, long idFormField)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.RemoveStaffFormField(idStaffForm, idFormField);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }

        [HttpPost("addStaffFormField")]
        public CommonResponse AddStaffFormField([FromBody]AddStaffFormFieldRequest request)
        {
            var response = new CommonResponse();
            try
            {
                response = bussinnessLayer.AddStaffFormField(request.IdStaffForm, request.Name, request.Description, request.Placeholder, request.DataType, request.Constraints);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }
            return response;
        }
    }
}