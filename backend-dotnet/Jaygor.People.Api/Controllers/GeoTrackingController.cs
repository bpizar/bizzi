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
using JayGor.People.Entities.Responses;
using JayGor.People.Bussinness;
using JayGor.People.Entities.Requests;
using JayGor.People.DataAccess;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class GeoTrackingController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public GeoTrackingController(IDatabaseService ds)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }

        [HttpPost("getgeotracking")]
        public GetGeoTrackingResponse GetGeoTacking([FromBody] GetGeoTrackingRequest request)
		{
            var response = new GetGeoTrackingResponse();

            try
            {
                var staffAux = new List<StaffForGeoTrackingCustomEntity>();
                var geoTimeTrackingAux = new List<GeoTimeTracking>();

                var geoTimeTrackingAutoAux = new List<GeoTimeTrackingAuto>();

                bussinnessLayer.GetGeoTacking(request.IdPeriod, request.Datex, request.GetAuto, out staffAux,out geoTimeTrackingAux,out geoTimeTrackingAutoAux);
                response.StaffForGeoTrackingList = staffAux;
                response.GeoTimeTrackingList = geoTimeTrackingAux;
                response.GeoTimeTrackingAuto = geoTimeTrackingAutoAux;
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