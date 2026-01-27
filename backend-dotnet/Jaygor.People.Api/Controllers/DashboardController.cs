using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JayGor.People.Bussinness;
using JayGor.People.ErrorManager;
using JayGor.People.Entities.Responses;
using JayGor.People.Entities.Requests;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.DataAccess;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class DashboardController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public DashboardController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }

        // public List<GenericPair> GetTasksForDashboard1(out string periodDescription)
        [HttpGet("gettasksfordashboard1")]
		public CommonResponse GetTasksForDashboard1()
		{
			var response = new CommonResponse();

			try
			{
				string periodDesc;
				response.Messages = bussinnessLayer.GetTasksForDashboard1(out periodDesc);
				response.TagInfo = periodDesc;
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}


		// public List<GenericPair> GetTasksForDashboard1(out string periodDescription)
		[HttpGet("getdashboard2")]
		public Dashboard2Response GetDashboard2()
		{
			var response = new Dashboard2Response();

			try
			{
				string periodDescAux;
                long maxAux;
                List<string> colorsAux;
				List<string> ProjectNameAux;
				List<long>  valuesAux;

                // response.Messages = bussinnessLayer.GetTasksForDashboard1(out periodDesc);
                // response.TagInfo = periodDesc;              
                bussinnessLayer.GetDashboard2(out periodDescAux,out maxAux,out colorsAux, out valuesAux, out ProjectNameAux);

				response.TagInfo = periodDescAux;
                response.MaxValue = maxAux;
                response.Colors = colorsAux;
                response.ProjectNames = ProjectNameAux;
                response.Values = valuesAux;
				response.Result = true;
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}

			return response;
		}


		// public List<GenericPair> GetTasksForDashboard1(out string periodDescription)
		[HttpGet("getdashboard3")]
		public Dashboard3Response GetDashboard3()
		{
			var response = new Dashboard3Response();

			try
			{
				//var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;

				string periodDescAux;
                response.Values = bussinnessLayer.GetDashboard3("", out periodDescAux);
                response.TagInfo = periodDescAux;
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

		