using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
		public long GetLastActivePeriod()
		{
			try
			{
                var periodByCurrent = context.periods.Where(c => c.State != "D" && DateTime.Now.CompareTo(c.From) >= 0 && DateTime.Now.CompareTo(c.To) <= 0).FirstOrDefault();

                if (periodByCurrent != null)
                {
                    return periodByCurrent.Id;
                }

                var lastPeriod = context.periods.Where(c=>c.State != "D") .OrderByDescending(c => c.Id).FirstOrDefault();

                if(lastPeriod!=null)
                {
                    return lastPeriod.Id;
                }						
			}
			catch (Exception ex)
			{
                throw ex;
			}
				
			return 0;
		}

        public GenericPair GetLastActivePeriodAndDesc()
        {
            try
            {
                var periodByCurrent = context.periods.Where(c => c.State != "D" && DateTime.Now.CompareTo(c.From) >= 0 && DateTime.Now.CompareTo(c.To) <= 0).FirstOrDefault();

                if (periodByCurrent != null)
                {
                    return new GenericPair() { Id = periodByCurrent.Id.ToString(), Description = string.Format("{0} - {1}", periodByCurrent.From.ToString(), periodByCurrent.To.ToString()) };
                }

                var lastPeriod = context.periods.Where(c => c.State != "D").OrderByDescending(c => c.Id).FirstOrDefault();

                if (lastPeriod != null)
                {
                    return new GenericPair() { Id = lastPeriod.Id.ToString(), Description = string.Format("{0} - {1}", lastPeriod.From.ToString(), lastPeriod.To.ToString()) };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new GenericPair() { Id = "0", Description = "-" };
        }


        public PeriodsCustom GetPeriod(long id)
        {
            return context.periods.Where(c => c.State != "D" && c.Id == id)
                        .Select(p => new PeriodsCustom
                        {
                            Id = p.Id,
                            CreationDate = p.CreationDate,
                            Description = p.Description,
                            State = p.State,
                            Abm = string.Empty,
                            IdfCreatedBy = p.IdfCreatedBy,                  
                        DateJoin = string.Format("{0} - {1}", Convert.ToDateTime(p.From).AddDays(1).ToString("MMM dd, yy"), Convert.ToDateTime(p.To).ToString("MMM, dd yy")),
                            From = p.From,
                            To = p.To
                        }).Single();               
        }

        public IEnumerable<PeriodsCustom> GetPeriods()
        {
            return context.periods.Where(c => c.State != "D")
                        .Select(p => new PeriodsCustom
                        {
                            Id = p.Id,
                            CreationDate = p.CreationDate,
                            Description = p.Description,
                            State = p.State,
                            Abm = string.Empty,
                            IdfCreatedBy = p.IdfCreatedBy,
                            DateJoin = string.Format("{0} - {1} {2}", Convert.ToDateTime(p.From, System.Globalization.CultureInfo.CurrentCulture).ToString("MMM dd yy"), Convert.ToDateTime(p.To).ToString("MMM dd yy"), p.State == "CL" ? "(Closed)" : ""),
                            From = p.From,
                            To = p.To
                        }).ToList();               
        }

        public CommonResponse SavePeriods(List<PeriodsCustom> periods, int workingHoursDefaultByPeriodStaff)
        {
            var transaction = context.Database.BeginTransaction();
            var result = new CommonResponse();

            try
            {
                foreach (var tod in periods)
                {
                    switch (tod.Abm)
                    {
                        case "D":
                            context.periods.Where(c => c.Id == tod.Id).FirstOrDefault().State = "D";
                            break;
                        case "E":
                            var edit = context.periods.Where(c => c.Id == tod.Id).FirstOrDefault();
                            edit.From = tod.From;
                            edit.To = tod.To;
                            edit.State = tod.State;
                            break;
                        case "I":

                            var lastPeriod = context.periods.OrderByDescending(c => c.Id).FirstOrDefault();

                            var newPeriod = new periods
                            {
                                CreationDate = DateTime.Now,
                                Description = string.Empty,
                                From = tod.From,
                                To = tod.To,
                                IdfCreatedBy = 0,
                                State = "C"
                            };

                            context.periods.Add(newPeriod);
                            context.SaveChanges();

                            if(lastPeriod!=null)
                            {
                                foreach(var spp in context.staff_project_position.Where(c=>c.IdfPeriod == lastPeriod.Id && c.State!="D"))
                                {
                                    var newSpp = new staff_project_position()
                                    {
                                        IdfStaff = spp.IdfStaff,
                                        IdfProject = spp.IdfProject,
                                        IdfPosition = spp.IdfPosition,
                                        IdfPeriod = newPeriod.Id,
                                        State = "C",
                                        Hours = 0
                                    };

                                    context.staff_project_position.Add(newSpp);
                                }

                                foreach(var po in context.project_owners.Where(c=>c.IdfPeriod == lastPeriod.Id && c.State!="D"))
                                {
                                    var newPo = new project_owners()
                                    {
                                        IdfProject = po.IdfProject,
                                        IdfOwner = po.IdfOwner,
                                        State = "C",
                                        IdfPeriod = newPeriod.Id
                                    };

                                    context.project_owners.Add(newPo);
                                }

                                foreach(var pc in context.projects_clients.Where(c => c.IdfPeriod == lastPeriod.Id && c.State!="D"))
                                {
                                    var newPc = new projects_clients()
                                    {
                                        IdfProject = pc.IdfProject,
                                        IdfClient = pc.IdfClient,
                                        IdfPeriod = newPeriod.Id,
                                        State = "C"
                                    };

                                    context.projects_clients.Add(newPc);
                                }

                                foreach (var pc in context.staff_period_settings.Where(c => c.IdfPeriod == lastPeriod.Id))
                                {
                                    var newSset = new staff_period_settings()
                                    {
                                        IdfPeriod = newPeriod.Id,
                                        IdfStaff = pc.IdfStaff,
                                        WorkingHours =  pc.WorkingHours
                                    };

                                    context.staff_period_settings.Add(newSset);
                                }
                            }
                            else
                            {
                                foreach (var stf in context.staff.Where(c=>c.State!="D"))
                                {
                                    var newSset = new staff_period_settings()
                                    {

                                        IdfPeriod = newPeriod.Id,
                                        IdfStaff = stf.Id,
                                        WorkingHours = workingHoursDefaultByPeriodStaff
                                    };

                                    context.staff_period_settings.Add(newSset);
                                }
                            }

                            break;
                    }

                    context.SaveChanges();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return result;
        }
    }
}