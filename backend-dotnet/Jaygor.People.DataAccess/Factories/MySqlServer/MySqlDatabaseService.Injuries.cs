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
        public List<InjuriesCustom> GetInjuriesListByClient(long idClient, long periodId)
        {
            return context.h_injuries
                                    .Include(c=>c.Project)
                                    .Where(c => c.IdfPeriod == periodId && c.IdfClient == idClient && c.State != "D")
                                    .Select(x => new InjuriesCustom
                                    {
                                        Id = x.Id,
                                        Color =   x.Project.Color, 
                                        DateInjury = x.DateOfInjury.ToString("MM/dd/yyyy hh:mm tt"),
                                        Description = x.DescName,
                                        Abm = string.Empty,
                                        ProjectName = x.Project.ProjectName,
                                        IdfIncident = x.IdfIncident
                                    }).ToList();               
        }

        public h_injuries GetInjuryById(long idInjury)
        {
            return context.h_injuries
                            .Where(c => c.Id == idInjury)
                            .Select(x => new h_injuries
                            {
                                Id = x.Id,
                                IdfPeriod = x.IdfPeriod,
                                IdfClient = x.IdfClient,
                                IdfIncident = x.IdfIncident,
                                IdfDegreeOfInjury = x.IdfDegreeOfInjury,
                                DescName = x.DescName,
                                DateOfInjury = x.DateOfInjury,
                                DateReportedSupervisor = x.DateReportedSupervisor,
                                IdfSupervisor = x.IdfSupervisor,
                                State = x.State,
                                BodySerialized = x.BodySerialized,
                                ProjectId = x.ProjectId
                            }).Single();               
        }

        public List<h_catalogCustom> GetInjuryCatalog()
        {
            return context.h_catalog
                                    .Where(c => c.State != "D" && c.Type == "J")
                                    .Select(p => new h_catalogCustom
                                    {
                                        id = p.id,
                                        Description = p.Description,
                                        IdentifierGroup = p.IdentifierGroup,
                                        Title = p.Description,
                                        Value = string.Empty,
                                        State = p.State,
                                        Type = p.Type
                                    }).ToList();               
        }


        public List<h_catalogCustom> GetInjuriesCatalog()
        {
            return context.h_catalog
                                    .Where(c => c.State != "D" && c.Type == "J")
                                    .Select(p => new h_catalogCustom
                                    {
                                        id = p.id,
                                        Description = p.Description,
                                        IdentifierGroup = p.IdentifierGroup,
                                        Title = p.Description,
                                        Value = string.Empty,
                                        State = p.State,
                                        Type = p.Type,
                                    }).ToList();               
        }


        public List<h_injury_values> GetCatalogInjuryValues(long idInjury)
        {
            return context.h_injury_values
                                    .Where(c => c.idfInjury == idInjury)
                                    .Select(p => new h_injury_values
                                    {
                                        id = p.id,
                                        idfCatalog = p.idfCatalog,
                                        Value = p.Value
                                    }).ToList();                
        }

        public bool SaveInjury(h_injuries Injury,
                                  List<h_injury_values> CatalogValues,
                                  List<List<PointBody>> Points,
                                  out long idInjuryOut)
        {

            var transaction = context.Database.BeginTransaction();
            idInjuryOut = 0;

            try
            {
              
                var injuryId = Injury.Id;
                var isNew = Injury.Id <= 0;


                if (isNew)
                {
                    Injury.DescName = CatalogValues.Single(c => c.idfCatalog == "st16_u").Value;
                    context.h_injuries.Add(Injury);
                    context.SaveChanges();
                    injuryId = Injury.Id;
                    CatalogValues.ForEach(c => c.idfInjury = injuryId);
                    context.h_injury_values.AddRange(CatalogValues);
                    idInjuryOut = injuryId;
                }
                else
                {
                    idInjuryOut = Injury.Id;
                    context.Update(Injury);
                    Injury.DescName = CatalogValues.Single(c => c.idfCatalog == "st16_u").Value;
                    context.h_injury_values.UpdateRange(CatalogValues);
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