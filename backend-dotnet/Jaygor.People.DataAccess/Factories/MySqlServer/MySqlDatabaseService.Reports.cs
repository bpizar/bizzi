//using JayGor.People.DataAccess.MySql;
using JayGor.People.Entities.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using JayGor.People.Entities.Entities;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public IEnumerable<ReportProjectsDetailsCustomEntity> GetReportProjects()
        {
            var result = new List<ReportProjectsDetailsCustomEntity>();

            result = context.tasks
                    .Where(c => c.IdfAssignedTo > 0 && c.State != "D")
                    .Select(s => new ReportProjectsDetailsCustomEntity
                    {
                        DeadLine = s.Deadline != null ? Convert.ToDateTime(s.Deadline).ToString("dd/MM/yyyy") : "",
                        ProjectName = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName,
                        AssignedToFullName = string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                        Hours = s.Hours.ToString(),
                        Period = string.Format("{0} - {1}", Convert.ToDateTime(context.periods.Where(c => c.Id == s.IdfPeriod && c.State != "D").Single().From).AddDays(1).ToString("dd/MM/yyyy"), Convert.ToDateTime(context.periods.Where(c => c.Id == s.IdfPeriod && c.State != "D").Single().To).ToString("dd/MM/yyyy")),
                        Seconds = s.Hours * 60 * 60,
                        Task = s.Subject
                    }).ToList();

            result.AddRange(context.tasks.Where(c => c.IdfAssignedTo == 0 && c.State != "D")
                     .Select(s => new ReportProjectsDetailsCustomEntity
                     {

                         DeadLine = s.Deadline != null ? Convert.ToDateTime(s.Deadline).ToString("dd/MM/yyyy") : "",
                         ProjectName = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName,
                         AssignedToFullName = "Unassigned",
                         Hours = s.Hours.ToString(),
                         Period = string.Format("{0} - {1}", Convert.ToDateTime(context.periods.Where(c => c.Id == s.IdfPeriod && c.State != "D").Single().From).AddDays(1).ToString("dd/MM/yyyy"), Convert.ToDateTime(context.periods.Where(c => c.Id == s.IdfPeriod && c.State != "D").Single().To).ToString("dd/MM/yyyy")),
                         Seconds = s.Hours * 60 * 60,
                         Task = s.Subject
                     }).ToList());

            return result;
        }

        public IEnumerable<tasks> GetReport1(List<long> projectIds, DateTime From, DateTime To)
        {
            var result = new List<tasks>();

            if (projectIds.Any())
            {
                result = context.tasks
                                .Include(c => c.IdfAssignedToNavigation).ThenInclude(c => c.IdfPositionNavigation)
                                .Include(c => c.IdfAssignedToNavigation).ThenInclude(c => c.IdfProject)
                                .Where(c => c.State != "D" && projectIds.Contains(c.IdfAssignedToNavigation.IdfProjectNavigation.Id))
                    .ToList();
            }
            else
            {
                result = context.tasks
                                .Include(c => c.IdfAssignedToNavigation).ThenInclude(c => c.IdfPositionNavigation)
                                .Include(c => c.IdfAssignedToNavigation).ThenInclude(c => c.IdfProject)
                    .Where(c => c.State != "D")
                    .ToList();
            }


            return result;
        }


        public List<TaskHistoryReportCustomEntity> GetTaskHistoryReport(long idProject, long idPeriod)
        {
           return context.tasks_state_history
                            .Where(c => c.IdfTaskNavigation.IdfProject == idProject && c.IdfTaskNavigation.IdfPeriod == idPeriod)
                           .OrderBy(c => c.Id)
                           .Select(p => new TaskHistoryReportCustomEntity
                           {
                               Task = p.IdfTaskNavigation.Subject,
                               Participant = string.Format("{0} {1}", p.IdfUserNavigation.LastName, p.IdfUserNavigation.FirstName),
                               Date = String.Format("{0:yyyy/MM/dd hh:mm tt}", p.CurrentDate),
                               Status = p.IdfStateNavigation.status
                           }).ToList();
        }
    }
}