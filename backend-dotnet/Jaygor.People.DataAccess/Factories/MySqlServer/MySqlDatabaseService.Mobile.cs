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
    
        public List<ScheduleMobile> GetScheduleByUser(long idUser, DateTime date1, DateTime date2)
        {
            return context.scheduling
                                    .Where(c => c.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id == idUser && c.From >= date1 && c.To <= date2)
                                    .Select(p => new ScheduleMobile
                                    {
                                        Id = p.Id,
                        From = Convert.ToDateTime(p.From),//.AddSeconds(1),
                                        To = Convert.ToDateTime(p.To),
                                        ProjectName = p.IdfProjectNavigation.ProjectName,
                                        Color = p.IdfProjectNavigation.Color
                                    }).ToList();                
        }

        public List<StaffProjectPositionCustomEntity> GetProjectsPositionsbyUser(long idUser, long idCurrentPeriod)
        {
           return context.staff_project_position
                            .Where(c => c.IdfStaffNavigation.IdfUserNavigation.Id == idUser && c.IdfPeriod == idCurrentPeriod)
                            .Select(p => new StaffProjectPositionCustomEntity
                            {
                                IdStaffProjectPosition = p.Id,
                                Name = p.IdfPositionNavigation.Name,
                                positionId = p.IdfPosition,
                                projectId = p.IdfProject,
                                ProjectName = p.IdfProjectNavigation.ProjectName
                            }).ToList();
        }

        public List<MyTasks_Mobile> GetMyTasks_Mobile(long idUser, long currentPeriod)
        {
           return context.tasks
                    .Where(c => c.IdfPeriod == currentPeriod &&
                           c.IdfStatus != 5 && c.IdfStatus != 4 &&
                           c.IdfAssignedTo != null && c.IdfAssignableRol == null &&
                           c.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id == idUser)
                    .Select(p => new MyTasks_Mobile
                    {
                        id = p.Id,
                        AssignationType = "assigned",
                        Subject = p.Subject,
                        idfStatus = p.IdfStatus.ToString(),

                        Deadline = Convert.ToDateTime(p.Deadline),
                        position = p.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                        ProjectName = p.IdfProjectNavigation.ProjectName,
                        Color = p.IdfProjectNavigation.Color,
                        type = p.Type
                    }).ToList();
        }

        public List<ProjectsDailyLogs_Mobile> GetProjectsDailyLogs_Mobile(long idUser)
        {
            //var lastPeriod = context.periods.OrderByDescending(c => c.Id).FirstOrDefault();
            var lastPeriod = this.GetLastActivePeriod();

            if (lastPeriod >0)
            {
                return context.projects_clients
                           .Where(c => c.IdfProjectNavigation.State != "D" && 
                                  c.IdfProjectNavigation.Visible == 1 &&
                                  c.IdfPeriod == lastPeriod)
                           .Select(p => new ProjectsDailyLogs_Mobile
                           {
                               Id = p.IdfProjectNavigation.Id,
                               ProjectName = p.IdfProjectNavigation.ProjectName
                           }).ToList();
            }
            else
            {
                return new List<ProjectsDailyLogs_Mobile>();
            }
        }

        public List<ClientsDailyLogs_Mobile> GetClientsDailyLogs_Mobile(long idUser)
        {
            var lastPeriod = this.GetLastActivePeriod();

            if (lastPeriod > 0)
            {
                return context.projects_clients
                           .Where(c => c.State != "D" && c.IdfClientNavigation.Active == 1 &&
                    c.IdfClientNavigation.State == "A" &&
                            c.IdfPeriod == lastPeriod)
                           .Select(p => new ClientsDailyLogs_Mobile
                           {
                               Id = p.IdfClientNavigation.Id,
                               ClientName = string.Format("{0} {1}", p.IdfClientNavigation.LastName, p.IdfClientNavigation.FirstName),
                               ProjectId = p.IdfProject
                           }).ToList();
            }   
            else
            {
                return new List<ClientsDailyLogs_Mobile>();
            }

        }

        public List<TimeTracker_Mobile> GetTimeTracker_Mobile(long idUser, DateTime date1)
        {
            return context.time_tracking
                                    .Include(i=>i.IdfStaffProjectPositionNavigation).ThenInclude(t=>t.IdfStaffNavigation)
                                    .Where(c => c.IdfStaffProjectPositionNavigation.IdfStaffNavigation.IdfUserNavigation.Id == idUser &&
                                           c.start >= date1)
                                    .Select(p => new TimeTracker_Mobile
                                    {
                                        id = p.Id,
                                        start = p.start,
                                        end = p.end,
                                        Color = p.IdfStaffProjectPositionNavigation.IdfProjectNavigation.Color,
                                        ProjectName = p.IdfStaffProjectPositionNavigation.IdfProjectNavigation.ProjectName
                                    }).ToList();                
        }

        public List<DailyLogs_Mobile> GetDailyLogsByUserId_Mobile(long idUser)
        {
            var lastPeriod = this.GetLastActivePeriod();

            if (lastPeriod > 0)
            {

                return context.h_dailylogs
                            .Where(c => c.UserId == idUser && c.IdfPeriod == lastPeriod && c.State != "D")
                            .Select(p => new DailyLogs_Mobile
                            {
                                Id = p.Id,
                                ProjectId = p.ProjectId,
                                ClientId = p.ClientId,
                                Date = p.Date,
                                UserId = p.UserId,
                                Placement = p.Placement,
                                StaffOnShift = p.StaffOnShift,
                                GeneralMood = p.GeneralMood,
                                InteractionStaff = p.InteractionStaff,
                                InteractionPeers = p.InteractionPeers,
                                School = p.School,
                                Attended = p.Attended,
                                InHouseProg = p.InHouseProg,
                                Comments = p.Comments,
                                Health = p.Health,
                                ContactFamily = p.ContactFamily,
                                SeriousOccurrence = p.SeriousOccurrence,
                                Other = p.Other,
                                clientName = string.Format("{0} {1}", p.Client.LastName, p.Client.FirstName),
                                ProjectName = p.Project.ProjectName,
                                Color = p.Project.Color
                            }).ToList();
            }
            else
            {
                return new List<DailyLogs_Mobile>();
            }
        }

        public bool SaveAutoGeoTracking(long idfUser, float latitude, float longitude, DateTime date1)
        {           
            var newTta = new time_tracking_auto
            {
                IdfUser = idfUser,
                Latitude = latitude,
                Longitude = longitude,
                start = date1
            };

            context.time_tracking_auto.Add(newTta);
            context.SaveChanges();

            return true;
        }

        public bool ChangeTaskState(long IdfState, long IdTask, long IdUser, DateTime date1)
        {
            var transaction = context.Database.BeginTransaction();

            try
            {
                var task = context.tasks.Where(c => c.Id == IdTask).Single();
                task.IdfStatus = IdfState;

                var newTsh = new tasks_state_history
                {
                    IdfUser = IdUser,
                    CurrentDate = date1,
                    IdfState = IdfState,
                    IdfTask = IdTask

                };

                context.tasks_state_history.Add(newTsh);
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

        public bool CheckIfStaffProjectPositionInLastPeriod(long idspp)
        {
            var transaction = context.Database.BeginTransaction();

            var lastPeriod = this.GetLastActivePeriod();

            if (lastPeriod > 0)
            {
                return false;
            }

            return context.staff_project_position.Where(c => c.Id == idspp && c.IdfPeriod == lastPeriod).FirstOrDefault() != null;
        }

		public bool StartTimeTracker(long IdfStaffProjectPosition, string startNote, float Longitude, float Latitude, DateTime date1)
		{
            var transaction = context.Database.BeginTransaction();

			try
			{
                var n = new time_tracking
                {
                    IdfStaffProjectPosition = IdfStaffProjectPosition,
                    start = date1,
                    status = 1,
                    startNote = startNote,
                    Longitude = Longitude,
                    Latitude = Latitude
                };

                context.time_tracking.Add(n);

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


        public bool StopTimeTracker(long Id, string endNote, float endLong, float endLat, DateTime date1)
        {			
            var u = context.time_tracking.Where(c => c.Id == Id).Single();

            u.end = date1;
            u.status = 2;
            u.endNote = endNote;
            u.endLong = endLong;
            u.endLat = endLat;

            context.SaveChanges();			
			return true;
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
							string State,
                            DateTime date1)
        {
            if(Id<0)
            {

                //var lastPeriod = context.periods.OrderByDescending(c => c.Id).FirstOrDefault();
                var lastPeriod = this.GetLastActivePeriod();

                if (lastPeriod  > 0)
                {
                    var newD = new h_dailylogs
                    {
                        ProjectId = ProjectId,
                        ClientId = ClientId,
                        Date = date1,
                        UserId = UserId,
                        Placement = Placement,
                        StaffOnShift = StaffOnShift,
                        GeneralMood = GeneralMood,
                        InteractionStaff = InteractionStaff,
                        InteractionPeers = InteractionPeers,
                        School = School,
                        Attended = Attended,
                        InHouseProg = InHouseProg,
                        Comments = Comments,
                        Health = Health,
                        ContactFamily = ContactFamily,
                        SeriousOccurrence = SeriousOccurrence,
                        Other = Other,
                        State = State,
                        IdfPeriod = lastPeriod
                    };

                    context.h_dailylogs.Add(newD);
                }
			}
            else{
                var u = context.h_dailylogs.Where(c => c.Id == Id).Single();

                u.ProjectId = ProjectId;
                u.ClientId = ClientId;
                u.Date = date1;
                u.UserId = UserId;
                u.Placement = Placement;
                u.StaffOnShift = StaffOnShift;
                u.GeneralMood = GeneralMood;
                u.InteractionStaff = InteractionStaff;
                u.InteractionPeers = InteractionPeers;
                u.School = School;
                u.Attended = Attended;
                u.InHouseProg = InHouseProg;
                u.Comments = Comments;
                u.Health = Health;
                u.ContactFamily = ContactFamily;
                u.SeriousOccurrence = SeriousOccurrence;
                u.Other = Other;
                u.State = State;
            }

            context.SaveChanges();
				
			return true;
        }

        public bool SaveFace(string email, string face)
        {			
            var u = context.identity_users.Where(c => c.Email == email).Single();
            u.Face = face;
            u.FaceStamp = Guid.NewGuid().ToString();
            context.SaveChanges();
            return true;
        }

    }
}