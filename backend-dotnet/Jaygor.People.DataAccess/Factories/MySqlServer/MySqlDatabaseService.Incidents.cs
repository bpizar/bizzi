using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public List<IncidentCustomEntity> GetIncidentsListByPeriod(long periodId)
        {
           return context.h_incidents
                                    .Where(c => c.IdfPeriod == periodId && c.State != "D")
                                    .Select(p => new IncidentCustomEntity
                                    {
                                        Id = p.id,
                                        Color = "",
                                        DateIncident = p.DateIncident.ToString("MM/dd/yyyy HH:mm"),
                                        Description = p.DescName,
                                        Abm = string.Empty,
                                        ProjectName = "", 
                                    }).ToList();                
        }

        public h_incidents GetIncidentById(long incidentId)
        {
           return context.h_incidents
                                    .Where(c => c.id == incidentId && c.State != "D")
                                    .Select(p => new h_incidents
                                    {
                                        id = p.id,
                                        DateIncident = p.DateIncident,
                                        TimeIncident = p.TimeIncident,
                                        IsSeriousOcurrence = p.IsSeriousOcurrence,
                                        IdfRegion = p.IdfRegion,
                                        DateTimeWhenSeriousOccurrence = p.DateTimeWhenSeriousOccurrence,
                                        SentToMinistry = p.SentToMinistry,
                                        IdfMinistry = p.IdfMinistry,
                                        IdfPeriod = p.IdfPeriod,
                                        IdfTypeOfSeriousOccurrence = p.IdfTypeOfSeriousOccurrence,
                                        IdfUmabIntervention = p.IdfUmabIntervention,
                                        State = p.State
                                    }).Single();
        }

        public List<h_incident_involved_people> GetIncidentInvolvedPeopleById(long incidentId)
        {
            return context.h_incident_involved_people
                                    .Where(c => c.idfIncident == incidentId && c.State != "D")
                                    .Select(x => new h_incident_involved_people
                                    {
                                        id = x.id,
                                        IdentifierGroup = x.IdentifierGroup,
                                        idfIncident = x.idfIncident,
                                        IdfSPP = x.IdfSPP,
                                        State = x.State
                                    }).ToList();
        }

        public List<h_degree_of_injury> GetDegreeOfInjuries()
        {
                return context.h_degree_of_injury
                                    .Where(c => c.State != "D")
                                    .Select(p => new h_degree_of_injury
                                    {
                                        id = p.id,
                                        Description = p.Description,
                                    }).ToList();                
        }

        public List<h_region> GetRegionInjuries()
        {
                return context.h_region
                                    .Where(c => c.State != "D")
                                    .Select(p => new h_region
                                    {
                                        id = p.id,
                                        Description = p.Description,
                                    }).ToList();                
        }


        public List<h_ministeries> GetMinisteries()
        {
            return context.h_ministeries
                                    .Where(c => c.State != "D")
                                    .Select(p => new h_ministeries
                                    {
                                        Id = p.Id,
                                        Description = p.Description
                                    }).ToList();
        }

        public List<h_catalogCustom> GetIncidentCatalog()
        {
                return context.h_catalog
                            .Where(c => c.State != "D" && c.Type == "I")
                            .Select(p => new h_catalogCustom
                            {
                                id = p.id,
                                Description = p.Description,
                                IdentifierGroup = p.IdentifierGroup,
                                Title = p.Description,
                                Value = string.Empty,
                                State = p.State
                            }).ToList();              
        }

        public List<h_incident_values> GetCatalogIncidentValues(long idIncident)
        {
            return context.h_incident_values
                                    .Where(c => c.idfIncident == idIncident)
                                    .Select(p => new h_incident_values
                                    {
                                        id = p.id,
                                        idfCatalog = p.idfCatalog,
                                        Value = p.Value
                                    }).ToList();                
        }

        public List<h_injuries> GetInjuriesByIdIncident(long idincident)
        {
                return context.h_injuries
                                    .Where(c => c.State != "D" && c.IdfIncident == idincident)
                                    .Select(x => new h_injuries
                                    {
                                        Id = x.Id,
                                        IdfIncident = idincident,
                                        IdfClient = x.IdfClient,
                                        DescName = string.Format("{0} - {1} {2}", x.DateOfInjury.ToString("MM/dd/yyyy HH:mm"), x.IdfClientNavigation.LastName, x.IdfClientNavigation.FirstName),
                                        State = x.State,
                                        IdfPeriod = x.IdfPeriod
                                          
                                    }).ToList();               
        }

        public List<h_type_serious_occurrence> GetTypeSeriousOccurrence()
        {
           return context.h_type_serious_occurrence
                                    .Where(c => c.State != "D")
                                    .Select(x => new h_type_serious_occurrence
                                    {
                                        Id = x.Id,
                                        Description = x.Description
                                    }).ToList();                
        }


        public List<h_umab_intervention> GetUmabIntervention()
        {
           return context.h_umab_intervention
                                    .Where(c => c.State != "D")
                                    .Select(x => new h_umab_intervention
                                    {
                                        Id = x.Id,
                                        Description = x.Description
                                    }).ToList();                
        }


        public bool SaveIncident(h_incidents Incident,
                                 List<h_incident_values> CatalogValues,
                                 List<ClientCustomEntity> Clients,
                                 List<h_injuries> Injuries,
                                 List<h_incident_involved_people> InvolvedPeople,
                                 out long idincidentOut)
        {
            var transaction = context.Database.BeginTransaction();

            idincidentOut = 0;

            try
            {
                var incidentId = Incident.id;
                var isNew = incidentId <= 0;



                idincidentOut = Incident.id;

                Incident.DescName = CatalogValues.Single(c => c.idfCatalog == "st1_u").Value;

                Incident.DescName = Incident.DescName.Length > 255 ? string.Format("{0}...", Incident.DescName.Substring(0,252)) : Incident.DescName;

                if (isNew)
                {
                    // Incident
                    context.h_incidents.Add(Incident);
                    context.SaveChanges();
                    idincidentOut = Incident.id;

                    // Involved People.
                    InvolvedPeople.ForEach(c => c.idfIncident = Incident.id);
                    context.h_incident_involved_people.AddRange(InvolvedPeople);

                    // Catalog Values.                           
                    CatalogValues.ForEach(c => c.idfIncident = Incident.id);
                    context.h_incident_values.AddRange(CatalogValues);

                    // Clients  and injuries
                    foreach (var c in Clients)
                    {
                        var h_clients_incidentAux = context.h_clients_incident.Where(x => x.IdfClient == c.IdfClient && x.IdfIncident == Incident.id && x.State == "C");


                        if (h_clients_incidentAux == null || h_clients_incidentAux.Count() <= 0)
                        {
                            context.h_clients_incident.Add(new h_clients_incident
                            {
                                IdfClient = c.IdfClient,
                                IdfIncident = Incident.id,
                                State = "C"
                            });
                        }

                        context.h_injuries.Where(x => x.IdfPeriod == Incident.IdfPeriod && x.IdfClient == c.IdfClient).ToList().ForEach(f => f.IdfIncident = Incident.id);
                    }
                }
                else
                {
                    // Update Incident.
                    context.Update(Incident);

                    // Save Incident Values.
                    context.h_incident_values.UpdateRange(CatalogValues);
                    context.h_incident_involved_people.UpdateRange(InvolvedPeople);


                    foreach(var inj in Injuries)
                    {
                        context.h_injuries.Where(c => c.Id == inj.Id).Single().IdfIncident = null;
                    }


                    foreach(var c in Clients)
                    {                                
                        if(c.Abm == "D")
                        {
                            context.h_clients_incident
                                .Where(l => l.IdfClient == c.IdfClient && l.IdfIncident == Incident.id)
                                .ToList()
                                .ForEach(x => x.State = "D");

                            context.h_injuries.Where(x => x.IdfPeriod == Incident.IdfPeriod && x.IdfClient == c.IdfClient).ToList().ForEach(f => f.IdfIncident = null);
                        }

                        if(c.Abm == "I")
                        {
                            var h_clients_incidentAux = context.h_clients_incident.Where(x => x.IdfClient == c.IdfClient && x.IdfIncident == Incident.id && x.State == "C");

                            if (h_clients_incidentAux == null || h_clients_incidentAux.Count() <= 0)
                            {
                                context.h_clients_incident.Add(new h_clients_incident
                                {
                                    IdfClient = c.IdfClient,
                                    IdfIncident = Incident.id,
                                    State = "C"
                                });
                            }

                            context.h_injuries.Where(x => x.IdfPeriod == Incident.IdfPeriod && x.IdfClient == c.IdfClient).ToList().ForEach(f => f.IdfIncident = Incident.id);
                        }
                    }
                }

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
                
            return true;
        }

    }
}