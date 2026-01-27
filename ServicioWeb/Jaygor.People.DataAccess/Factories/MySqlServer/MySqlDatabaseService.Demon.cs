//using JayGor.People.DataAccess.MySql;
using JayGor.People.Entities.CustomEntities;
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
        public List<tasks_reminders> GetTaskReminderByCurrentTime()
        {
			return context.tasks_reminders
                           .Where(c => c.State == "C" && 
                                  c.IdfTaskNavigation.IdfAssignedTo != null &&
                                  c.IdfTaskNavigation.Deadline.Value.ToShortDateString() == DateTime.Now.ToShortDateString() &&
                                  new DateTime(c.IdfTaskNavigation.Deadline.Value.Year,c.IdfTaskNavigation.Deadline.Value.Month,c.IdfTaskNavigation.Deadline.Value.Day,c.IdfTaskNavigation.Deadline.Value.Hour,c.IdfTaskNavigation.Deadline.Value.Minute,0)
                                  .CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute,0).AddMinutes(c.IdfSettingReminderTimeNavigation.MinutesBefore)) == 0 &&
                                  c.IdfTaskNavigation.State == "C" && 
                                  c.IdfTaskNavigation.IdfStatus != 4 && 
                                  c.IdfTaskNavigation.IdfStatus != 5)
                           .Include(v=>v.IdfTaskNavigation)
                                    .ThenInclude(n=>n.IdfProjectNavigation)
                           .Include(c => c.IdfTaskNavigation)
                                .ThenInclude(a => a.IdfAssignedToNavigation)
                                .ThenInclude(b => b.IdfStaffNavigation)
                                .ThenInclude(c => c.IdfUserNavigation)
                           .Include(d => d.IdfSettingReminderTimeNavigation)
                                    .ToList();
		}

        public List<h_medical_remindersCustom> GetMedicalRemindersByCurrentTime()
        {
              return context.h_medical_reminders.Where(c => c.State != "D" && c.Reminder == 1 &&
                           (                                                        
                               (new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day, c.Datetime.Hour,c.Datetime.Minute,0))
                               .CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0))==0                                                          
                              )
                              &&
                              (
                                  (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, c.Datetime.Hour, c.Datetime.Minute, 0))
                                  .CompareTo(new DateTime(c.From.Year, c.From.Month, c.From.Day, 0, 0, 1)) >= 0

                                  &&

                                   (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, c.Datetime.Hour, c.Datetime.Minute, 0))
                                  .CompareTo(new DateTime(c.To.Year, c.To.Month, c.To.Day, 11, 59, 59)) <= 0
                              )
                           )
                          .Select(x => new h_medical_remindersCustom
                           {
                               Id = x.Id,
                               Color = x.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                               ProjectName = x.IdfAssignedToNavigation.IdfProjectNavigation.Description,
                               Description = string.Format("{0}", x.Description),
                               Client = string.Format("{0} {1}", x.IdfClientNavigation.LastName, x.IdfClientNavigation.FirstName),
                               IdfAssignedTo = x.IdfAssignedTo,
                               IdfClient = x.IdfClient,
                               Reminder = x.Reminder,
                               SppDescription = string.Format("{0} {1} - {2}", x.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, x.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName, x.IdfAssignedToNavigation.IdfPositionNavigation.Name),
                               abm = string.Empty,
                               Datetime = x.Datetime,
                               From = x.From,
                               To = x.To,
                               State = x.State,
                               IdUser = x.IdfAssignedToNavigation.IdfStaffNavigation.Id
                           }
                      ).ToList();
        }

    }
}