using JayGor.People.Bussinness;
using JayGor.People.Entities.Responses;
using JayGor.People.ErrorManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using JayGor.People.Entities.Requests;
using JayGor.People.DataAccess;

namespace Jaygor.People.Api.Controllers
{
    [Route("[controller]")]
    public class DailyLogController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer;
        private readonly ILogger _logger;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public DailyLogController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILogger<IncidentsController> logger, IDatabaseService ds)
        {
            _env = env;
            _logger = logger;
            this.bussinnessLayer = new BussinnessLayer(ds);
        }

        [HttpGet("getdailylogbyid/{iddailylog}/{idperiod}/{idclient}/{timeDifference}")]
        public GetDailyLogByIdResponse GetDailyLogById(long iddailylog, long idperiod, long idclient, int timeDifference)
        {
            var response = new GetDailyLogByIdResponse();

            try
            {
                response.CurrentDateTime = bussinnessLayer.FixTime(response.CurrentDateTime, timeDifference);
                

                var DailyLogAux = new h_dailylogs();
                var InvolvedPeopleAux = new List<h_dailylog_involved_people>();
                //out List<StaffCustomEntity> Staffs
                var StaffsAux = new List<StaffCustomEntity>();

                var clientNameAux = string.Empty;
                var clientImgAux = string.Empty;

                var projectsAux = new List<ProjectCustomEntity>(); // GetProjectClientByIdPeriodIdClient

                bussinnessLayer.GetDailyLogById(iddailylog, idperiod , idclient, out DailyLogAux, out InvolvedPeopleAux, out StaffsAux, out clientNameAux, out clientImgAux, out projectsAux);

                response.DailyLog = DailyLogAux;
                response.InvolvedPeople.AddRange(InvolvedPeopleAux);
                response.Staffs.AddRange(StaffsAux);
                response.ClientName = clientNameAux;
                response.ClientImg = clientImgAux;
                response.Projects.AddRange(projectsAux);

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [Authorize(Roles = "admin,projecteditor")]
        [HttpPost("savedailylog")]
        public CommonResponse SaveDailyLog([FromBody]SaveDailyLogRequest request)
        {
            var response = new CommonResponse();

            try
            {
                long idDailyLogAux = 0;
                // bussinnessLayer.SaveInjury(request.Injury, request.Catalog, request.Points);
                response.Result  = bussinnessLayer.SaveDailyLog(request.DailyLog, request.InvolvedPeople, request.TimeDifference, out idDailyLogAux);
                response.TagInfo = idDailyLogAux.ToString();
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

    }
}