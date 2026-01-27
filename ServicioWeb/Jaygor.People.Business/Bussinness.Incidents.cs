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
        public List<IncidentCustomEntity> GetIncidentsListByPeriod(long periodId)
        {
            return dataAccessLayer.GetIncidentsListByPeriod(periodId).OrderByDescending(c=>c.DateIncident).ToList();
        }

        public List<h_injuries> GetInjuriesByIdIncident(long idincident)
        {
            return dataAccessLayer.GetInjuriesByIdIncident(idincident);
        }

        public bool getIncidentById(long idincident,
            long idperiod,
             //out ClientCustomEntity client,
             out h_incidents incident,
             out List<ProjectCustomEntity> projects,
             //out List<h_injury_type> injuryTypes,
             out List<h_degree_of_injury> degreeOfInjuries,
             out List<h_region> regions,
             out List<h_ministeries> ministeries,
             out List<h_catalogCustom> catalog,
             out List<ClientCustomEntity> clients,
             out List<h_injuries> Injuries,
             out List<StaffCustomEntity> staffs,
             out List<h_incident_involved_people> involvedPeople,
             out List<h_type_serious_occurrence> typeSeriousOccurrence,
             out List<h_umab_intervention> umabIntervention)
        {

            var isnew = idincident < 0;



            incident = new h_incidents();
            projects = new List<ProjectCustomEntity>();                     
            degreeOfInjuries = new List<h_degree_of_injury>();
            regions = new List<h_region>();
            ministeries = new List<h_ministeries>();
            catalog = new List<h_catalogCustom>();
            clients = new List<ClientCustomEntity>();
            Injuries = new List<h_injuries>();
            staffs = new List<StaffCustomEntity>();
            involvedPeople = new List<h_incident_involved_people>();
            typeSeriousOccurrence = new List<h_type_serious_occurrence>();
            umabIntervention = new List<h_umab_intervention>();

            projects = dataAccessLayer.GetProjects().ToList();
            degreeOfInjuries = dataAccessLayer.GetDegreeOfInjuries();
            regions = dataAccessLayer.GetRegionInjuries();
            ministeries = dataAccessLayer.GetMinisteries();
            umabIntervention = dataAccessLayer.GetUmabIntervention();
            typeSeriousOccurrence = dataAccessLayer.GetTypeSeriousOccurrence();
            staffs = dataAccessLayer.GetStaffForIncident(idperiod).ToList();

            var cat = dataAccessLayer.GetIncidentCatalog();

            if(isnew)
            {
                incident.DateIncident = DateTime.Now;
                incident.DateTimeWhenSeriousOccurrence = DateTime.Now;
                incident.IdfPeriod = idperiod;

                //Injury.Id = -1;
               // catalog.AddRange(cat);
            }
            else
            {

                incident = dataAccessLayer.GetIncidentById(idincident);
                clients = dataAccessLayer.GetClientsByIncident(idincident);
                Injuries = dataAccessLayer.GetInjuriesByIdIncident(idincident);
                involvedPeople = dataAccessLayer.GetIncidentInvolvedPeopleById(idincident);

                // staffs = dataAccessLayer.GetStaffForIncident(incident.IdfPeriod).ToList();
                var catval = dataAccessLayer.GetCatalogIncidentValues(idincident);

                foreach (h_incident_values cv in catval)
                {
                    var found = cat.Where(c => c.id == cv.idfCatalog);

                    if (found != null)
                    {
                        found.Single().Value = cv.Value;
                    }
                }

            }          

            catalog.AddRange(cat);

            return true;
        }

        public bool SaveIncident(h_incidents Incident,
                                 List<h_catalogCustom> Catalog,
                                 List<ClientCustomEntity> Clients,
                                 List<h_injuries> Injuries,
                                 List<h_incident_involved_people> InvolvedPeople,
                                 out long idincident,
                                 out List<h_injuries> injuriesAux)
        {

            idincident = 0;

            injuriesAux = new List<h_injuries>();

            var catalogValues = Catalog.Select(x => new
            h_incident_values
            {
                id = x.IdValue,
                idfCatalog = x.id,
                idfIncident = Incident.id,
                Value = x.Value
            }).ToList();


            dataAccessLayer.SaveIncident(Incident, catalogValues, Clients, Injuries, InvolvedPeople, out idincident);
            injuriesAux = dataAccessLayer.GetInjuriesByIdIncident(idincident);

            return true;
            //return dataAccessLayer.SaveIncident(Incident, catalogValues, Clients, Injuries, InvolvedPeople, out idincident);
        }
    

    }
}