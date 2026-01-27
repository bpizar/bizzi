using JayGor.People.Bussinness;
using JayGor.People.Entities.Responses;
using JayGor.People.ErrorManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.DataAccess;

namespace JayGor.People.Api.Controllers
{
    [Route("[controller]")]
    public class ClientsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public ClientsController(IDatabaseService ds, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            bussinnessLayer = new BussinnessLayer(ds);
        }

    
        //private readonly ILogger _logger;

        //private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        //public ClientsController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILogger<ClientsController> logger)
        //{
        //    _env = env;
        //    _logger = logger;
        //}

        [HttpGet("getallclients")]
        public GetAllClientsResponse GetAllClients()
        {
            var response = new GetAllClientsResponse();

            try
            {
                var clientssAux = new List<ClientCustomEntity>();
                //var positionsAux = new List<Position>();
                this.bussinnessLayer.GetAllClients(out clientssAux/*, out positionsAux*/);
                response.clients = clientssAux;
                /*response.Positions = positionsAux;*/
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


        [HttpGet("getallclientslist")]
        public GetAllClientsListResponse GetAllClientsList()
        {
            var response = new GetAllClientsListResponse();
            try
            {
                var clientssAux = new List<ClientCustomEntity>();
                //var positionsAux = new List<Position>();
                this.bussinnessLayer.GetAllClients(out clientssAux/*, out positionsAux*/);
                clientssAux.ForEach(p => {
                    response.clients.Add(new ClientListCustomEntity()
                    {
                        Id = p.Id,
                        FullName = p.FullName,
                        BirthDate = p.BirthDate,
                        PhoneNumber = p.PhoneNumber,
                        Email = p.Email,
                        Notes = p.Notes,
                        Img = p.Img,
                        ProgramInfo = p.ProgramInfo
                    });
                });
                /*response.Positions = positionsAux;*/
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpGet("getclient/{id}")]
        public GetClientResponse GetClient(long id)
        {
            var response = new GetClientResponse();

            try
            {
                List<h_medical_remindersCustom> medicalRemindersAux = new List<h_medical_remindersCustom>();
                var staffAux = new List<StaffCustomEntity>();

                response.Client =  bussinnessLayer.GetClientbyId(id, out medicalRemindersAux, out staffAux);
                response.Reminders.AddRange(medicalRemindersAux);
                response.Staff.AddRange(staffAux);
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }

        [HttpPost]
        [Route("saveclient")]
        public SaveClientResponse SaveClient([FromBody]SaveClientRequest request)
        {
            var response = new SaveClientResponse();

            try
            {
                var isNew = request.Client.Id == -1;

                if (isNew)
                {
                    // var user = bussinnessLayer.GetClientByEmail(request.Client.Email);

                    // if (user != null)
                    // {
                    //   response.Result = false;
                    //     response.Messages.Add(new GenericPair { Id = "10002", Description = "The account is in use" });
                    //     return response;
                    // }
                }

                // response.TagInfo = bussinnessLayer.SaveClient( request.Client, request.Reminders).TagInfo;
                response = bussinnessLayer.SaveClient(request.Client, request.Reminders, request.ProjectClient);               
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }

            return response;
        }


        [HttpGet("getclientdatabyperiodid/{idperiod}/{idclient}")]
        public GetClientDataByPeriodIdResponse_Mobile GetClientDataByPeriodId(long idperiod, long idclient)
        {
            var response = new GetClientDataByPeriodIdResponse_Mobile();

            try
            {
                var DailyLogListAux = new List<DailyLogCustomEntity>();              
                var InjuriesAux = new List<InjuriesCustom>();
                var ProjectClientAux = new List<ProjectClientCustomEntity>();
                var StaffListAux = new List<StaffCustomEntity>();
             
                bussinnessLayer.GetClientDataByPeriodId(idperiod, 
                                                        idclient,                                                      
                                                        out ProjectClientAux,
                                                        out StaffListAux,
                                                        out DailyLogListAux,                                                        
                                                        out InjuriesAux);

                response.DailyLogsList = new List<DailyLogCustomEntity>();                
                response.InjuriesList = new List<InjuriesCustom>();
                response.ProjectClient = new List<ProjectClientCustomEntity>();
                response.StaffList = new List<StaffCustomEntity>();


                response.DailyLogsList.AddRange(DailyLogListAux);
                response.InjuriesList.AddRange(InjuriesAux);

                response.ProjectClient.AddRange(ProjectClientAux);
                response.StaffList.AddRange(StaffListAux);

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