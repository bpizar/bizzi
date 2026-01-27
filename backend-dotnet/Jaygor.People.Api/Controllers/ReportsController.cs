using JayGor.People.Bussinness;
using JayGor.People.Entities.Responses;
using JayGor.People.ErrorManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;
using JayGor.People.DataAccess;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class ReportsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public ReportsController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }

        [HttpPost("getreport1")]
        public GetReport1Response GetReport1([FromBody]GetReport1Request request)
        {
            var response = new GetReport1Response();

            try
            {
                response.Tasks = bussinnessLayer.GetReport1(request.ProjectIds, request.From, request.To).ToList();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getreportprojects")]
        public GetReportProjectsResponse GetReportProjects()
        {
            var response = new GetReportProjectsResponse();

            try
            {
                response.Details = bussinnessLayer.GetReportProjects().ToList();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


		[HttpGet("gettaskhistoryreport/{idproject}/{idperiod}")]
		public TaskHistoryReportResponse GetTaskHistoryReport(long idProject, long idPeriod)
		{
			var response = new TaskHistoryReportResponse();

			try
			{
				var tasksAux = new List<TaskHistoryReportCustomEntity>();

				response.tasks = bussinnessLayer.GetTaskHistoryReport(idProject, idPeriod);

				//response.Tasks = tasksAux;
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