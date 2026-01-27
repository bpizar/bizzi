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
        public IEnumerable<TimeTrackingReviewCustom> GetTimeTrackingReviewResponse(long idPeriod)
        {
            var transaction = context.Database.BeginTransaction();

            try
            {
                context.time_tracking_review.Where(c => c.IdfPeriod == idPeriod).ToList().ForEach(c => c.State = "D");

                var sppList = context.staff_project_position
                         .Join(context.scheduling,
                                 spp => spp.Id,
                                 s => s.IdfAssignedTo,
                                 (spp, s) => new { spp, s })
                          .Where(c => c.s.State != "D" && c.spp.State != "D" && c.s.IdfPeriod == idPeriod)
                          .Select(s => new Staff_Project_PositionCustomEntity
                          {
                              Id = s.spp.Id,
                              IdfPosition = s.spp.IdfPosition,
                              IdfStaff = s.spp.IdfStaff,
                              IdfProject = s.spp.IdfProject
                          }).Distinct().ToList();

                var periodAux = context.periods.Where(c => c.Id == idPeriod && c.State != "D").Single();

                foreach (var spp in sppList)
                {
                var newRow = new time_tracking_review();

                    newRow.IdfPeriod = idPeriod;
                    newRow.IdfStaffProjectPosition = spp.Id;

                newRow.SecondsScheduledTime = Convert.ToInt64(context.scheduling.ToList()
                                              .Where(c => c.IdfAssignedTo == spp.Id &&
                                                                c.State != "D" &&
                                                                c.IdfProject == spp.IdfProject &&
                                                                c.IdfPeriod == idPeriod)
                                                              .Sum(x => Convert.ToDateTime(x.To).Subtract(Convert.ToDateTime(x.From)).TotalSeconds));


                newRow.SecondsUserTracking = Convert.ToInt64(context.time_tracking.ToList()
                                                        .Where(c => c.IdfStaffProjectPosition == spp.Id &&
                                                                    spp.State != "D" &&
                                                                    //c.idfProject == spp.IdfProject &&
                                                               Convert.ToDateTime(c.start) >= periodAux.From &&
                                                                    c.end != null &&
                                                                    c.end <= periodAux.To)
                                                             .Sum(s => Convert.ToDateTime(s.end).Subtract(Convert.ToDateTime(s.start)).TotalSeconds));

                newRow.SecondsModifiedTracking = newRow.SecondsUserTracking;
                newRow.State = "C";

                var record = context.time_tracking_review.Where(c => c.IdfStaffProjectPosition == spp.Id && c.IdfPeriod == idPeriod).FirstOrDefault();

                if (record != null)
                {
                    record.SecondsScheduledTime = newRow.SecondsScheduledTime;
                    record.SecondsUserTracking = newRow.SecondsUserTracking;
                    record.State = "C";
                    //todo review if change in database.
                }
                else
                {
                    context.time_tracking_review.Add(newRow);
                }
            }

            context.SaveChanges();

            transaction.Commit();

            return context.time_tracking_review
                            .Where(c => c.IdfPeriod == idPeriod && c.State != "D")
                            .Select(s => new TimeTrackingReviewCustom
                            {
                                Id = s.Id,
                                IdfPeriod = s.IdfPeriod,
                                IdfStaffProjectPosition = s.IdfStaffProjectPosition,
                                SecondsModifiedTracking = s.SecondsModifiedTracking,
                                SecondsScheduledTime = s.SecondsScheduledTime,
                                SecondsUserTracking = s.SecondsUserTracking,
                                State = s.State,
                                Abm = string.Empty,
                                ModifiedTrackingFormat = s.SecondsModifiedTracking.ToString(),
                                ParticipantFullName = string.Format("{0} {1}", s.IdfStaffProjectPositionNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfStaffProjectPositionNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                PositionName = s.IdfStaffProjectPositionNavigation.IdfPositionNavigation.Name,
                                ScheduledTimeFormat = s.SecondsScheduledTime.ToString(),
                                UserTrackingFormat = s.SecondsUserTracking.ToString(),
                                ProjectName = s.IdfStaffProjectPositionNavigation.IdfProjectNavigation.ProjectName,
                                ProjectColor = string.Format("#{0}", s.IdfStaffProjectPositionNavigation.IdfProjectNavigation.Color),
                                Img = s.IdfStaffProjectPositionNavigation.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c=>c.Id== s.IdfStaffProjectPositionNavigation.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name,
                IdfProject = s.IdfStaffProjectPositionNavigation.IdfProject
                            }).ToList();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public CommonResponse SaveTimeTrackingReview(List<TimeTrackingReviewCustom> tracking)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
                      
            try
            {                        
                foreach (var track in tracking)
                {
                    var edit = context.time_tracking_review.Where(c => c.Id == track.Id && c.State != "D").Single();
                    edit.SecondsModifiedTracking = track.SecondsModifiedTracking;
                }

                context.SaveChanges();
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