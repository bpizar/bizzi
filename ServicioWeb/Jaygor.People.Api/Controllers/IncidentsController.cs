
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
using Microsoft.AspNetCore.Authorization;
using JayGor.People.DataAccess;

namespace Jaygor.People.Api.Controllers
{
    [Route("[controller]")]
    public class IncidentsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();
        private readonly ILogger _logger;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public IncidentsController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILogger<IncidentsController> logger, IDatabaseService  ds)
        {
            _env = env;
            _logger = logger;
            this.bussinnessLayer = new BussinnessLayer(ds);
        }

        [HttpGet("getincidentslistbyperiod/{idperiod}")]
        public GetIncidentsListByPeriodResponse GetIncidentsListByPeriod(long idperiod)
        {
            var response = new GetIncidentsListByPeriodResponse();

            try
            {
                var clientssAux = new List<ClientCustomEntity>();
               
                response.Incidents = bussinnessLayer.GetIncidentsListByPeriod(idperiod);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getIncidentbyid/{idincident}/{idperiod}")]
        public getIncidentByIdResponse getIncidentById(long idincident, long idperiod)
        {
            var response = new getIncidentByIdResponse();

            try
            {
                var clientAux = new ClientCustomEntity();
                var incidentAux = new h_incidents();
                var projectsAux = new List<ProjectCustomEntity>();
               // var injuryTypesAux = new List<h_injury_type>();
                var degreeOfInjuriesAux = new List<h_degree_of_injury>();
                var regionsAux = new List<h_region>();
                var ministeriesAux = new List<h_ministeries>();
                var catalogAux = new List<h_catalogCustom>();
                var clientsAux = new List<ClientCustomEntity>();
                var injuriesAux = new List<h_injuries>();

                var staffsAux = new List<StaffCustomEntity>();

                var involvedPeopleAux = new List<h_incident_involved_people>();

                var typeSeriousOccurrenceAux = new List<h_type_serious_occurrence>();

                //  out List<h_umab_intervention> umabIntervention
                var umabInterventionAux = new List<h_umab_intervention>();

                bussinnessLayer.getIncidentById(idincident,
                                                idperiod,
                                                out incidentAux,
                                                out projectsAux,
                                                //out injuryTypesAux,
                                                out degreeOfInjuriesAux,
                                                out regionsAux,
                                                out ministeriesAux,
                                                out catalogAux,
                                                out clientsAux,
                                                out injuriesAux,
                                                out staffsAux,
                                                out involvedPeopleAux,
                                                out typeSeriousOccurrenceAux,
                                                out umabInterventionAux);

                // response.client = clientAux;
                response.Incident = incidentAux;
                response.ProjectsList.AddRange(projectsAux);

                // response.InjuryTypeList.AddRange(injuryTypesAux);
                response.DegreeOfInjuryList.AddRange(degreeOfInjuriesAux);
                response.RegionList.AddRange(regionsAux);
                response.Catalog.AddRange(catalogAux);
                response.Clients.AddRange(clientsAux);
                response.Injuries.AddRange(injuriesAux);
                response.Staffs.AddRange(staffsAux);
                response.InvolvedPeople.AddRange(involvedPeopleAux);
                response.Ministeries.AddRange(ministeriesAux);
                response.TypeSeriousOccurrence.AddRange(typeSeriousOccurrenceAux);
                response.UmabIntervention.AddRange(umabInterventionAux);

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


        [Authorize(Roles = "admin,projecteditor")]
        [HttpPost("saveincident")]
        public SaveIncidentResponse SaveIncident([FromBody]SaveIncidentRequest request)
        {
            var response = new SaveIncidentResponse();

            try
            {
                long idIncidentAux = 0;
                List<h_injuries> injuriesAux = new List<h_injuries>();

                response.Result = bussinnessLayer.SaveIncident(request.Incident, request.Catalog, request.Clients, request.Injuries, request.InvolvedPeople, out idIncidentAux, out injuriesAux);
                response.Injuries.AddRange(injuriesAux);
                response.TagInfo = idIncidentAux.ToString();
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


    }
}