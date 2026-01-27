using System;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{

    public static class CommonX
    {
       
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
             (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

    }


    public partial class BussinnessLayer
    {
		// public List<ScheduleMobile> Schedule { get; set; }
		public List<ScheduleMobile> GetScheduleByUser(long idUser)
        {
            var date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var date2 = date1.AddDays(14);
            return dataAccessLayer.GetScheduleByUser(idUser, date1, date2).OrderBy(c => c.From).ToList(); ;
        }
	
        public List<StaffProjectPositionCustomEntity> GetUserProjectsPositions(long idUser)
        {
            var idperiod = dataAccessLayer.GetLastActivePeriodAndDesc().Id; //.GetLastActivePeriod();

            //if()

            return dataAccessLayer.GetProjectsPositionsbyUser(idUser,Convert.ToInt64(idperiod));
        }

        public List<MyTasks_Mobile> GetMyTasks_Mobile(long idUser)
        {
			var idperiod = dataAccessLayer.GetLastActivePeriod();
            return dataAccessLayer.GetMyTasks_Mobile(idUser,idperiod);
        }


        ////TODO: usar en common.
        //private  IEnumerable<TSource> DistinctBy<TSource, TKey>
        // (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        //{
        //    HashSet<TKey> knownKeys = new HashSet<TKey>();
        //    foreach (TSource element in source)
        //    {
        //        if (knownKeys.Add(keySelector(element)))
        //        {
        //            yield return element;
        //        }
        //    }
        //}


        public List<ProjectsDailyLogs_Mobile> GetProjectsDailyLogs_Mobile(long idUser)
        {
            // var res = dataAccessLayer.GetProjectsDailyLogs_Mobile(idUser)
            //                     .Distinct();
            // return res.Distinct().ToList();
            // return dataAccessLayer.GetProjectsDailyLogs_Mobile(idUser)
            //                    .Distinct()
            //                  .ToList();
            // return this.DistinctBy(dataAccessLayer.GetProjectsDailyLogs_Mobile(idUser), c => c.Id).ToList();
            return dataAccessLayer.GetProjectsDailyLogs_Mobile(idUser).DistinctBy(x=>x.Id).ToList();
        }

        public List<ClientsDailyLogs_Mobile> GetClientsDailyLogsResponse_Mobile(long idUser)
        {
            return dataAccessLayer.GetClientsDailyLogs_Mobile(idUser).DistinctBy(x => x.Id).ToList(); ;
        }


        public List<TimeTracker_Mobile> GetTimeTracker_Mobile(long idUser)
        {
            var d = DateTime.Now;
            var date1 = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
            return dataAccessLayer.GetTimeTracker_Mobile(idUser, date1);
        }

        public List<DailyLogs_Mobile> GetDailyLogsByUserId_Mobile(long idUser)
        {
            return dataAccessLayer.GetDailyLogsByUserId_Mobile(idUser);
        }

        public bool SaveAutoGeoTracking(long idfUser, float latitude, float longitude)
        {
            var date1 = DateTime.Now;
            return dataAccessLayer.SaveAutoGeoTracking(idfUser, latitude, longitude, date1);
        }

        public bool ChangeTaskState(long IdfState, long IdTask, long IdUser)
        {
			var date1 = DateTime.Now;
            return dataAccessLayer.ChangeTaskState(IdfState, IdTask, IdUser, date1);
        }

        public bool StartTimeTracker(long IdfStaffProjectPosition, string startNote, float Longitude, float Latitude)
        {
            var date1 = DateTime.Now;
            return dataAccessLayer.StartTimeTracker(IdfStaffProjectPosition, startNote,Longitude,Latitude,date1);
        }

		public bool StopTimeTracker(long Id, string endNote, float endLong, float endLat)
		{
			var date1 = DateTime.Now;
			return dataAccessLayer.StopTimeTracker( Id,  endNote,  endLong,  endLat, date1);
		}

		public bool SaveDailyLog(long Id,
							long ProjectId,
							long ClientId,
							long UserId,
							string Placement,
							string StaffOnShift,
							string GeneralMood,
							string InteractionStaff,
							string InteractionPeers,
							string School,
							string Attended,
							string InHouseProg,
							string Comments,
							string Health,
							string ContactFamily,
							string SeriousOccurrence,
							string Other,
							string State)
        {
            var date1 = DateTime.Now;
            return dataAccessLayer.SaveDailyLog( Id,
                    							 ProjectId,
                    							 ClientId,
                    							 UserId,
                    							 Placement,
                    							 StaffOnShift,
                    							 GeneralMood,
                    							 InteractionStaff,
                    							 InteractionPeers,
                    							 School,
                    							 Attended,
                    							 InHouseProg,
                    							 Comments,
                    							 Health,
                    							 ContactFamily,
                    							 SeriousOccurrence,
                    							 Other,
                    							 State,
                                                 date1);
        }

        public bool SaveFace(string email, string face)
        {
            return dataAccessLayer.SaveFace(email, face);
        }

        public bool CheckIfStaffProjectPositionInLastPeriod(long idspp)
        {
            return dataAccessLayer.CheckIfStaffProjectPositionInLastPeriod(idspp);
        }

       

    }
}