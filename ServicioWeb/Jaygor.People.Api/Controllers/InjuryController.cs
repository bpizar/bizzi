using JayGor.People.Bussinness;
using JayGor.People.Entities.Responses;
using JayGor.People.ErrorManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using JayGor.People.DataAccess;

//using Jaygor.People.Api.helpers;


namespace Jaygor.People.Api.Controllers
{
    [Route("[controller]")]
    public class InjuryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();
        private readonly ILogger _logger;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        public InjuryController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILogger<IncidentsController> logger, IDatabaseService ds)
        {
            _env = env;
            _logger = logger;
            this.bussinnessLayer = new BussinnessLayer(ds);
        }



        //[HttpGet("getdailylogbyid/{iddailylog}/{idperiod}/{idclient}")]
        //public GetDailyLogByIdResponse GetDailyLogById(long iddailylog, long idperiod, long idclient)

        [HttpGet("getinjurybyid/{idinjury}/{idperiod}/{idclient}/{timeDifference}")]
        public GetInjuryByIdResponse GetInjuryById(long idinjury, long idperiod, long idclient,int timeDifference)
        {
            var response = new GetInjuryByIdResponse();

            try
            {
                response.CurrentDateTime = bussinnessLayer.FixTime(response.CurrentDateTime, timeDifference);

                var InjuryAux = new h_injuries();
                var DegreeOfInjuryListAux = new List<h_degree_of_injury>();
                var StaffsAux = new List<StaffCustomEntity>();
                var CatalogAux = new List<h_catalogCustom>();
                var clientNameAux = string.Empty;
                var clientImgAux = string.Empty;

                var pointsAux = new List<List<PointBody>>();
                var projectsAux = new List<ProjectCustomEntity>();

                bussinnessLayer.GetInjuryById(idperiod,
                                              idinjury,
                                              idclient,
                                              //timeDifference,
                                              out InjuryAux,
                                              out DegreeOfInjuryListAux,
                                              out StaffsAux,
                                              out CatalogAux,
                                              out clientNameAux,
                                              out clientImgAux,
                                              out pointsAux,
                                              out projectsAux);

                response.Injury = InjuryAux;
                response.DegreeOfInjuryList.AddRange(DegreeOfInjuryListAux);
                response.Staffs.AddRange(StaffsAux);
                response.Catalog.AddRange(CatalogAux);
                response.ClientName = clientNameAux;
                response.ClientImg = clientImgAux;
                response.Projects.AddRange(projectsAux);

                response.Points = new List<List<PointBody>>();

                response.Points.AddRange(pointsAux);

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [Authorize(Roles = "admin,projecteditor")]
        [HttpPost("saveinjury")]
        public CommonResponse SaveInjury([FromBody]SaveInjuryRequest request)
        {
            var response = new CommonResponse();

            try
            {
                long idInjuryOut = 0;
                response.Result = bussinnessLayer.SaveInjury(request.Injury, request.Catalog, request.Points,request.TimeDifference, out idInjuryOut);
                response.TagInfo = idInjuryOut.ToString();
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

    }
}