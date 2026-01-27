using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public SaveClientResponse SaveClient(clients client, List<h_medical_remindersCustom> Reminders, List<ProjectClientCustomEntity> ProjectClient)
        {
            var response = new SaveClientResponse();
           response.TagInfo = dataAccessLayer.SaveClient(client, Reminders, ProjectClient).TagInfo ;
            response.Reminders = dataAccessLayer.GetMedicalRemindersByClient(Convert.ToInt64(response.TagInfo));

            return response;
        }

        // ClientCustomEntity GetClientById(long id, out List<h_medical_remindersCustom> medicalReminders)
        public ClientCustomEntity GetClientbyId(long id, out List<h_medical_remindersCustom> medicalReminders, out  List<StaffCustomEntity> staff)
        {
            var client = new ClientCustomEntity();
            medicalReminders = new List<h_medical_remindersCustom>();

            staff = new List<StaffCustomEntity>();

            if (id >= 0)
            {
                client = dataAccessLayer.GetClientById(id);
                medicalReminders = dataAccessLayer.GetMedicalRemindersByClient(id);
                staff = dataAccessLayer.GetStaffsByClient(id);
            }

            return client;
        }

        public void GetAllClients(out List<ClientCustomEntity> clients)
        {

            clients = dataAccessLayer.GetClients().ToList();

            foreach(var cli in clients)
            {
                var AssignedProgramsToAux = string.Empty;
                var projectsAux = dataAccessLayer.GetProjectsByClient(cli.Id, dataAccessLayer.GetLastActivePeriod());

                var s = string.Empty;

                if (projectsAux != null)
                {
                    // projectsAux.ToList().ForEach(c => s = string.Format("{0}" + (string.IsNullOrEmpty(s) ? "" : " , ") + "{1} {2}", s, c.ProjectName , (c.project_owners.FirstOrDefault() != null ? c.project_owners.FirstOrDefault().IdfOwnerNavigation.IdfUserNavigation.LastName + c.project_owners.FirstOrDefault().IdfOwnerNavigation.IdfUserNavigation.FirstName : "-")));
                    projectsAux.ToList().ForEach(c => s = string.Format("{0}" + (string.IsNullOrEmpty(s) ? "" : " , ") + "{1}", s, c.ProjectName));
                }

                AssignedProgramsToAux = s;
                cli.ProgramInfo = AssignedProgramsToAux;
            }

            // clients = dataAccessLayer.GetClients().ToList();
        }

        public ClientCustomEntity GetClientByEmail(string Email)
        {
            return dataAccessLayer.GetClientByEmail(Email);
        }

        public bool GetClientDataByPeriodId(long idperiod, 
                                            long idclient,
                                            out List<ProjectClientCustomEntity> ProjectClient,
                                            out List<StaffCustomEntity> StaffList,
                                            out List<DailyLogCustomEntity> DailyLogsList,                                           
                                            out List<InjuriesCustom> InjuriesList)
        {
            DailyLogsList = new List<DailyLogCustomEntity>();            
            InjuriesList = new List<InjuriesCustom>();

            // programs involved and responsable.
            ProjectClient = new List<ProjectClientCustomEntity>();
            // all staff positions
            StaffList = new List<StaffCustomEntity>();


            DailyLogsList = dataAccessLayer.GetDailyLogList(idperiod, idclient).OrderByDescending(c => c.DateDailyLog).ToList(); ;            
            InjuriesList = dataAccessLayer.GetInjuriesListByClient(idclient, idperiod).OrderByDescending(c=>c.DateInjury).ToList();

            StaffList = dataAccessLayer.GetStaffsByClient(idclient, idperiod);

            ProjectClient = dataAccessLayer.GetProjectClientByIdPeriodIdClient(idperiod, idclient).ToList();


            return true;
        }

        //public bool getIncidentById(long idincident,
        //      //out ClientCustomEntity client,
        //      out h_incidents incident,
        //      out List<ProjectCustomEntity> projects,
        //      //out List<h_injury_type> injuryTypes,
        //      out List<h_degree_of_injury> degreeOfInjuries,
        //      out List<h_region> regions,
        //      out List<h_ministeries> ministeries,
        //      out List<h_catalogCustom> catalog,
        //      out List<ClientCustomEntity> clients,
        //      out List<h_injuries> Injuries,
        //      out List<StaffCustomEntity> staffs,
        //      out List<h_incident_involved_people> involvedPeople,
        //      out List<h_type_serious_occurrence> typeSeriousOccurrence)
        //{
        //    // client = new ClientCustomEntity();
        //    incident = new h_incidents();

        //    projects = new List<ProjectCustomEntity>();

        //    // injuryTypes = new List<h_injury_type>();
        //    degreeOfInjuries = new List<h_degree_of_injury>();
        //    regions = new List<h_region>();
        //    ministeries = new List<h_ministeries>();
        //    catalog = new List<h_catalogCustom>();
        //    clients = new List<ClientCustomEntity>();
        //    Injuries = new List<h_injuries>();
        //    staffs = new List<StaffCustomEntity>();
        //    involvedPeople = new List<h_incident_involved_people>();

        //    typeSeriousOccurrence = new List<h_type_serious_occurrence>();

        //    incident = dataAccessLayer.GetIncidentById(idincident);

        //    // client = dataAccessLayer.GetClientById(idclient);
        //    projects = dataAccessLayer.GetProjects().ToList();

        //    // injuryTypes = dataAccessLayer.GetInjuryTypes();
        //    degreeOfInjuries = dataAccessLayer.GetDegreeOfInjuries();
        //    regions = dataAccessLayer.GetRegionInjuries();
        //    ministeries = dataAccessLayer.GetMinisteries();
        //    clients = dataAccessLayer.GetClientsByIncident(idincident);
        //    Injuries = dataAccessLayer.GetInjuriesByIdIncident(idincident);

        //    // IEnumerable<StaffForPlanningCustomEntity> GetStaffForIncident(long idperiod);
        //    staffs = dataAccessLayer.GetStaffForIncident(incident.IdfPeriod).ToList();

        //    involvedPeople = dataAccessLayer.GetIncidentInvolvedPeopleById(idincident);
        //    typeSeriousOccurrence = dataAccessLayer.GetTypeSeriousOccurrence();

        //    var cat = dataAccessLayer.GetIncidentCatalog();
        //    var catval = dataAccessLayer.GetIncidentValues(idincident);

        //    foreach(h_values cv in catval)
        //    {
        //        var found = cat.Where(c => c.Id == cv.IdfCatalog);

        //        if(found != null)
        //        {
        //            found.Single().Value = cv.Value;
        //        }
        //    }

        //    catalog.AddRange(cat);

        //    return true;
        //}

    }
}