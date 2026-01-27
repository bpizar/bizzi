using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public h_dailylogs GetDailyLogById(long idDailyLog)
        {
            return context.h_dailylogs
                             .Include(c => c.Client)
                            .Where(c => c.Id == idDailyLog)                                
                            .Select(x => new h_dailylogs
                            {
                                    Id = x.Id,
                                    IdfPeriod = x.IdfPeriod,
                                    Attended = x.Attended,
                                    ClientId = x.ClientId,
                                    Comments = x.Comments,
                                    ContactFamily = x.ContactFamily,
                                    Date = x.Date,
                                    GeneralMood = x.GeneralMood,
                                    Health = x.Health,
                                    InHouseProg = x.InHouseProg,
                                    InteractionPeers = x.InteractionPeers,
                                    InteractionStaff = x.InteractionStaff,
                                    Other = x.Other,
                                    Placement = x.Placement,
                                    ProjectId = x.ProjectId,
                                    School = x.School,
                                    SeriousOccurrence = x.SeriousOccurrence,
                                    UserId = x.UserId  ,
                                    State = x.State ,
                                    StaffOnShift = x.StaffOnShift                                    
                            }).Single();               
        }

        public List<h_dailylog_involved_people> GetDailyLogInvolvedPeopleById(long dailylogId)
        {
            return context.h_dailylog_involved_people
                            .Where(c => c.IdfDailyLog == dailylogId && c.State != "D")
                            .Select(x => new h_dailylog_involved_people
                            {
                                Id = x.Id,
                                IdentifierGroup = x.IdentifierGroup,
                                IdfDailyLog = x.IdfDailyLog,
                                IdfSPP = x.IdfSPP,
                                State = x.State
                            }).ToList();               
        }

        public bool SaveDailyLogs(h_dailylogs DailyLog,
                                  List<h_dailylog_involved_people> InvolvedPeople,
                                  out long idDailyLogOut)
        {
            var transaction = context.Database.BeginTransaction(); 

            idDailyLogOut = 0;

            try
            {
                var dailyLogId = DailyLog.Id;
                var isNew = dailyLogId <= 0;

                if (isNew)
                {
                    context.h_dailylogs.Add(DailyLog);
                    context.SaveChanges();

                    dailyLogId = DailyLog.Id;

                    InvolvedPeople.ForEach(c => c.IdfDailyLog = dailyLogId);
                    context.h_dailylog_involved_people.AddRange(InvolvedPeople);

                    idDailyLogOut = dailyLogId;
                }
                else
                {
                    idDailyLogOut = dailyLogId;
                    DailyLog.StaffOnShift = ".";
                    context.Update(DailyLog);


                    //var toAdd = InvolvedPeople.Where(c => c.Id == 0).ToList();
                    //var toUpdate = InvolvedPeople.Where(c => c.Id != 0).ToList();



                    //context.h_dailylog_involved_people.AddRange(toAdd);
                    //context.h_dailylog_involved_people.UpdateRange(toUpdate);
                    context.h_dailylog_involved_people.UpdateRange(InvolvedPeople);


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