using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JayGor.People.Bussinness;
using Microsoft.AspNetCore.Authorization;
using JayGor.People.ErrorManager;
using JayGor.People.Entities.Responses;
using JayGor.People.Entities.Requests;
using JayGor.People.DataAccess;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class PositionsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public PositionsController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }

        [HttpGet("getpositions")]
        public GetPositionsResponse GetPositions()
        {
            var response = new GetPositionsResponse();

            try
            {
                response.Positions = bussinnessLayer.GetPositions().ToList();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

		//[HttpGet("getpositionsroles")]
		//public GetPositionsRolesResponse GetPositionsRoles()
		//{
		//    var response = new GetPositionsRolesResponse();

		//    try
		//    {                
		//        response.PositionsRolesList = bussinnessLayer.GetPositionsRoles().ToList();
		//        response.Roles = bussinnessLayer.IdentityGetRoles().ToList();
		//        response.Positions = bussinnessLayer.GetPositions().ToList();
		//        response.Result = true;
		//    }
		//    catch (Exception ex)
		//    {
		//        response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
		//    }

		//    return response;
		//}

		//[Authorize(Roles = "admin")]
		//[HttpPost("savepositions")]
		//public CommonResponse SavePositions([FromBody]SavePositionsRolesRequest request)
		//{
		//    var response = new CommonResponse();
		//    var userRequesting = HttpContext.User;

		//    try
		//    {
		//        response = bussinnessLayer.SavePositions(request.Positions, request.PositionsRolesList);

		//        response.Result = true;
		//    }
		//    catch (Exception ex)
		//    {
		//        response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
		//    }

		//    return response;
		//}

		[Authorize(Roles = "positionsmanager")]
		[HttpPost("savepositions")]
		public CommonResponse SavePositions([FromBody]SavePositionsRolesRequest request)
		{
		    var response = new CommonResponse();
		   
		    try
		    {
		        response = bussinnessLayer.SavePositions(request.Positions);

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
