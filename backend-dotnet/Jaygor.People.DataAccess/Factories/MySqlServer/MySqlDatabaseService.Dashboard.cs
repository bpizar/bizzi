using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using Microsoft.EntityFrameworkCore;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
		public List<GenericPair> GetTasksForDashboard1(out string periodDescription)
		{		
			periodDescription = "None Period";

            var currentPeriodNumber = this.GetLastActivePeriod(); // context.periods.Where(c => c.State != "D").OrderByDescending(c => c.Id).FirstOrDefault();

            //var currentPeriod = context.periods.Where(c => c.State != "D" && c.Id == currentPeriodNumber).OrderByDescending(c => c.Id).FirstOrDefault();


            if (currentPeriodNumber > 0)
			{
                var currentPeriod = context.periods.Where(c => c.State != "D" && c.Id == currentPeriodNumber).OrderByDescending(c => c.Id).FirstOrDefault();

                periodDescription = String.Format("{0:yyyy/MM/dd} - {1:yyyy/MM/dd}", currentPeriod.From, currentPeriod.To);

				return context.tasks
								.Join(context.statuses,
									  task => task.IdfStatus,
									  status => status.Id, (task, status) => new { Task = task, Status = status })
								.Where(s => s.Task.State != "D" && s.Task.IdfPeriod == currentPeriodNumber)
								.GroupBy(c => c.Status.status)
								.Select(x => new GenericPair
								{
									Id = x.Key.ToString(),
									Description = x.Count().ToString()
								}).ToList();
            }
            else
            {
                return new List<GenericPair>();
            }
        }


		public List<tasks> GetDashboard2(out string periodDescription)
		{
			periodDescription = "None Period";

            //var currentPeriod = context.periods.Where(c => c.State != "D").OrderByDescending(c => c.Id).FirstOrDefault();
            var currentPeriodNumber = this.GetLastActivePeriod();

            if (currentPeriodNumber > 0)
			{
                var currentPeriod = context.periods.Where(c => c.State != "D" && c.Id == currentPeriodNumber).OrderByDescending(c => c.Id).FirstOrDefault();

                periodDescription = String.Format("{0:yyyy/MM/dd} - {1:yyyy/MM/dd}", currentPeriod.From, currentPeriod.To);

				return context
					.tasks
                    .Include(i=>i.IdfProjectNavigation)
					.Where(c => c.State != "D" && c.IdfPeriod == currentPeriod.Id && (c.IdfStatusNavigation.Id != 4 && c.IdfStatusNavigation.Id != 5))
					.OrderBy(o => o.IdfProjectNavigation.ProjectName)
					.ToList();
            }
            else {
                return new List<tasks>();
            }

        }

        public List<GenericTriValue> GetDashboard3(string username, out string periodDescription)
		{			
			periodDescription = "None Period";
            var result = new List<GenericTriValue>();
            var currentPeriodNumber = this.GetLastActivePeriod();

            if (currentPeriodNumber > 0)
            {
                var currentPeriod = context.periods.Where(c => c.State != "D" && c.Id == currentPeriodNumber).OrderByDescending(c => c.Id).FirstOrDefault();

                periodDescription = String.Format("{0:yyyy/MM/dd} - {1:yyyy/MM/dd}", currentPeriod.From, currentPeriod.To);

                result.Add(new GenericTriValue {
                        Id = periodDescription,
                        Description1 = context
                            .tasks
                            .Where(c => c.State != "D" && c.IdfPeriod == currentPeriod.Id && c.IdfStatusNavigation.Id != 4 && c.IdfAssignedTo == null)
                            .Count().ToString(),
                        Description2 = context
                            .tasks
                            .Where(c => c.State != "D" && c.IdfPeriod == currentPeriod.Id && c.IdfStatusNavigation.Id != 4 && c.IdfAssignedTo != null)
                            .Count().ToString()
                });
            }

            return result;
        }
	}
}