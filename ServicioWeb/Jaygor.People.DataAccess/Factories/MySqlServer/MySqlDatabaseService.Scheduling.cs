using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.DataAccess.MySql;
//using JayGor.People.DataAccess.MySql;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using Microsoft.EntityFrameworkCore;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {

        public bool DeleteSelectedSchedules(List<long> listaIdSchedules)
        {
            //var scheduled =
            context.scheduling.Where(c => listaIdSchedules.Contains(c.Id)).ToList()
                .ForEach(d => d.State = "D");
            //scheduled.State = "D";
            context.SaveChanges();

            return true;
        }


        public IEnumerable<SchedulingCustomEntity> GetSchedulingByOwnProjects(long period, string username)
        {
            var result = new List<SchedulingCustomEntity>();
            var idstaff = context.staff.Where(c => c.IdfUserNavigation.Email == username).FirstOrDefault().Id;
			var periodRow = this.GetPeriod(period);
			var moveOrResizeScheduled = periodRow.State != "CL";

            result = context.scheduling
                            .Join(context.projects,
                                        s => s.IdfProject,
                                        proy => proy.Id,
                                        (s, proy) => new { s, proy })
                                        .Where(cc => cc.proy.State != "D" && cc.s.State != "D" && cc.s.IdfPeriod == period)
                            .Join(context.project_owners.Where(c => c.State != "D" && c.IdfOwner == idstaff && c.IdfPeriod == period),
                                pro => pro.proy.Id,
                                po => po.IdfProject,
                                (pro, po) => new { pro, po })
                            .Where(cc => cc.po.State != "D" && cc.po.IdfPeriod == period) //OJO && cc.po.IdfOwner==idstaff en vez de lo de la linea 43  (.Join(context.Project_Owners.Where(c => c.State != "D" && c.IdfOwner == idstaff),)
                                .Select(s => new SchedulingCustomEntity
                                {
                                    Id = s.pro.s.Id,
                                    AllDay = s.pro.s.AllDay,
                                    From = s.pro.s.From,
                                    To = s.pro.s.To,
                                    State = s.pro.s.State,
                                    IdDuplicate = s.pro.s.IdDuplicate,
                                    IdfAssignedTo = s.pro.s.IdfAssignedTo,
                                    IdfStaff = s.pro.s.IdfAssignedToNavigation.IdfStaff,
                                    AssignedToPosition = s.pro.s.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                                    ProjectName = s.pro.s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,
                                    ProjectColor = s.pro.s.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                                    AssignedToFullName = string.Format("{0} {1}", s.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                    IdUser = s.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id,
                                    Hours = s.pro.s.To !=null ? Convert.ToInt64(Convert.ToDateTime(s.pro.s.To).Subtract(Convert.ToDateTime(s.pro.s.From)).TotalSeconds) : 0,
									Resizable = moveOrResizeScheduled,
									Draggable = moveOrResizeScheduled,
                                    Img = s.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name,
									subject = " ",
                                    IdfProject = s.pro.s.IdfAssignedToNavigation.IdfProjectNavigation.Id

                                }).ToList();

            result.AddRange(context.tasks
                  .Join(context.projects,
                                        s => s.IdfProject,
                                        proy => proy.Id,
                                        (s, proy) => new { s, proy })
                                        .Where(cc => cc.proy.State != "D" && cc.s.State != "D" && cc.s.IdfPeriod == period)
                            .Join(context.project_owners.Where(c => c.State != "D" && c.IdfOwner == idstaff && c.IdfPeriod == period),
                                pro => pro.proy.Id,
                                po => po.IdfProject,
                                (pro, po) => new { pro, po })
                         .Where(c => c.po.State != "D" && c.pro.s.Deadline != null && c.pro.s.IdfPeriod == period)
                         .Select(s => new SchedulingCustomEntity
                         {
                             // Id = s.pro.s.Id,
                             Id = s.pro.s.Id + 100000000,
                             From = s.pro.s.Deadline,
                             To = s.pro.s.Deadline,
                             State = "TT",
                             IdDuplicate = 0,
                             IdfAssignedTo = s.pro.s.IdfAssignedTo != null ? s.pro.s.IdfAssignedTo : 0,
                             IdfStaff = s.pro.s.IdfAssignedTo != null ? s.pro.s.IdfAssignedToNavigation.IdfStaff : 0,
                             AssignedToPosition = s.pro.s.IdfAssignedTo != null ? s.pro.s.IdfAssignedToNavigation.IdfPositionNavigation.Name : "Unassigned", // s.IdfAssignableRol != null ? s.IdfAssignableRolNavigation.Name : "Unassigned",// .IdfAssignedToNavigation.IdfPositionNavigation.Name : "",ProjectName = s.IdfProjectNavigation.ProjectName,// .IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,ProjectColor = s.IdfProjectNavigation.Color,// s.IdfAssignedToNavigation.IdfProjectNavigation.Color,AssignedToFullName = s.IdfAssignedTo != null ? string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName) : "",

                               ProjectName = s.pro.s.IdfProjectNavigation.ProjectName,// s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,
                               ProjectColor = s.pro.s.IdfProjectNavigation.Color, // s.IdfAssignedToNavigation.IdfProjectNavigation.Color,

                               AssignedToFullName = s.pro.s.IdfAssignedTo != null ? string.Format("{0} {1}", s.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName) : "Unassigned",

                             IdUser = s.pro.s.IdfAssignedTo != null ? s.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id : 0,
                             Hours = 0, // Convert.ToInt64(Convert.ToDateTime(s.To).Subtract(Convert.ToDateTime(s.From)).TotalSeconds),Resizable = false,
                               Resizable = false,
                             Draggable = false,
                             Img = s.pro.s.IdfAssignedTo != null ? s.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name : "generic",
                             subject = s.pro.s.Subject,
                             IdfProject = s.pro.s.IdfProjectNavigation.Id// s.IdfAssignedToNavigation.IdfProjectNavigation.Id
                           }).ToList());

            return result;
        }

        public IEnumerable<SchedulingCustomEntity> GetSchedulingByOwnScheduling(long period, string username)
        {
            var result = new List<SchedulingCustomEntity>();
            var idstaff = context.staff.Where(c => c.IdfUserNavigation.Email == username).FirstOrDefault().Id;

            result = context.scheduling.ToList()
                            .Where(c => c.State != "D" && c.IdfAssignedToNavigation.IdfStaff == idstaff && c.IdfPeriod == period)
                     .Select(s => new SchedulingCustomEntity
                     {
                         Id = s.Id,
                         AllDay = s.AllDay,
                         From = s.From,
                         To = s.To,
                         State = s.State,
                         IdDuplicate = s.IdDuplicate,
                         IdfAssignedTo = s.IdfAssignedTo,
                         IdfStaff = s.IdfAssignedToNavigation.IdfStaff,
                         AssignedToPosition = s.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                         ProjectName = s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,
                         ProjectColor = s.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                         AssignedToFullName = string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                         IdUser = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id,
                         Hours = Convert.ToInt64(Convert.ToDateTime(s.To).Subtract(Convert.ToDateTime(s.From)).TotalSeconds),
                         Resizable=false,
                         Draggable = false,
						 Img = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name,
						 subject = " ",
                         IdfProject = s.IdfAssignedToNavigation.IdfProjectNavigation.Id
                     }).ToList();


            result.AddRange(context.tasks
                         .Where(c => c.State != "D" && c.Deadline != null && c.IdfAssignedToNavigation.IdfStaff == idstaff && c.IdfPeriod == period)
                         .Select(s => new SchedulingCustomEntity
                         {
                             Id = s.Id + 100000000,
                             AllDay = 1,
                             From = s.Deadline,
                             To = s.Deadline,
                             State = "TT",
                             IdDuplicate = 0,
                             IdfAssignedTo = s.IdfAssignedTo != null ? s.IdfAssignedTo : 0,
                             IdfStaff = s.IdfAssignedTo != null ? s.IdfAssignedToNavigation.IdfStaff : 0,
                             AssignedToPosition = s.IdfAssignedTo != null ? s.IdfAssignedToNavigation.IdfPositionNavigation.Name : "Unassigned", // s.IdfAssignableRol != null ? s.IdfAssignableRolNavigation.Name : "Unassigned",// .IdfAssignedToNavigation.IdfPositionNavigation.Name : "",ProjectName = s.IdfProjectNavigation.ProjectName,// .IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,ProjectColor = s.IdfProjectNavigation.Color,// s.IdfAssignedToNavigation.IdfProjectNavigation.Color,AssignedToFullName = s.IdfAssignedTo != null ? string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName) : "",

                               ProjectName = s.IdfProjectNavigation.ProjectName,// s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,
                               ProjectColor = s.IdfProjectNavigation.Color, // s.IdfAssignedToNavigation.IdfProjectNavigation.Color,

                               AssignedToFullName = s.IdfAssignedTo != null ? string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName) : "Unassigned",

                             IdUser = s.IdfAssignedTo != null ? s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id : 0,
                             Hours = 0, // Convert.ToInt64(Convert.ToDateTime(s.To).Subtract(Convert.ToDateTime(s.From)).TotalSeconds),Resizable = false,
                               Resizable = false,
                             Draggable = false,
                             Img = s.IdfAssignedTo != null ? s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name : "generic",
                             subject = s.Subject,
                             IdfProject = s.IdfProjectNavigation.Id// s.IdfAssignedToNavigation.IdfProjectNavigation.Id
                           }).ToList());
                                          
            return result;
        }

        public IEnumerable<SchedulingCustomEntity> GetScheduling(long period)
        {               
            var result = new List<SchedulingCustomEntity>();

            var periodRow = this.GetPeriod(period);

            if (periodRow != null)
            {
                var moveOrResizeScheduled = periodRow.State != "CL";

                result = context.scheduling
                         .Where(c => c.State != "D" && c.IdfAssignedTo > 0 && c.IdfPeriod == period)
                         .Select(s => new SchedulingCustomEntity
                         {
                             Id = s.Id,
                             AllDay = s.AllDay,
                             From = s.From,
                             To = s.To,
                             State = s.State,
                             IdDuplicate = s.IdDuplicate,
                             IdfAssignedTo = s.IdfAssignedTo,
                             IdfStaff = s.IdfAssignedToNavigation.IdfStaff,
                             AssignedToPosition = s.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                             ProjectName = s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,
                             ProjectColor = s.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                             AssignedToFullName = string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                             IdUser = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id,
                             Hours = Convert.ToInt64(Convert.ToDateTime(s.To).Subtract(Convert.ToDateTime(s.From)).TotalSeconds),
                             Resizable = moveOrResizeScheduled,
                             Draggable = moveOrResizeScheduled,
                             Img = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name,
                             subject = " ",
                             IdfProject = s.IdfAssignedToNavigation.IdfProjectNavigation.Id
                         }).ToList();

                result.AddRange(context.tasks
                       .Where(c => c.State != "D" && c.Deadline != null && c.IdfPeriod == period)
                       .Select(s => new SchedulingCustomEntity
                       {
                           Id = s.Id + 100000000,
                           AllDay = 1,
                           From = s.Deadline,
                           To = s.Deadline,
                           State = "TT",
                           IdDuplicate = 0,
                           IdfAssignedTo = s.IdfAssignedTo != null ? s.IdfAssignedTo : 0,
                           IdfStaff = s.IdfAssignedTo != null ? s.IdfAssignedToNavigation.IdfStaff : 0,
                           AssignedToPosition = s.IdfAssignedTo != null ? s.IdfAssignedToNavigation.IdfPositionNavigation.Name : "Unassigned", // s.IdfAssignableRol != null ? s.IdfAssignableRolNavigation.Name : "Unassigned",// .IdfAssignedToNavigation.IdfPositionNavigation.Name : "",ProjectName = s.IdfProjectNavigation.ProjectName,// .IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,ProjectColor = s.IdfProjectNavigation.Color,// s.IdfAssignedToNavigation.IdfProjectNavigation.Color,AssignedToFullName = s.IdfAssignedTo != null ? string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName) : "",

                           ProjectName = s.IdfProjectNavigation.ProjectName,// s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,
                           ProjectColor = s.IdfProjectNavigation.Color, // s.IdfAssignedToNavigation.IdfProjectNavigation.Color,

                           AssignedToFullName = s.IdfAssignedTo != null ? string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName) : "Unassigned",

                           IdUser = s.IdfAssignedTo != null ? s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id : 0,
                           Hours = 0, // Convert.ToInt64(Convert.ToDateTime(s.To).Subtract(Convert.ToDateTime(s.From)).TotalSeconds),Resizable = false,
                           Resizable = false,
                           Draggable = false,
                           Img = s.IdfAssignedTo != null ? s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name : "generic",
                           subject = s.Subject,
                           IdfProject = s.IdfProjectNavigation.Id// s.IdfAssignedToNavigation.IdfProjectNavigation.Id
                       }).ToList());

            }

            return result;
        }

        public IEnumerable<SchedulingCustomEntity> GetSchedulingByProject(long project, long period)
        {
            var periodRow = this.GetPeriod(period);
            var moveOrResizeScheduled = periodRow.State != "CL";

            return context.scheduling
                     .Where(c => c.State != "D" && c.IdfProject == project && c.IdfPeriod == period)
                     .Select(s => new SchedulingCustomEntity
                     {
                         Id = s.Id,
                         AllDay = s.AllDay,
                         From = s.From,
                         To = s.To,
                         State = s.State,
                         IdDuplicate = s.IdDuplicate,
                         IdfAssignedTo = s.IdfAssignedTo,
                         IdfStaff = s.IdfAssignedToNavigation.IdfStaff,
                         AssignedToPosition = s.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                         ProjectName = s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,
                         ProjectColor = s.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                         AssignedToFullName = string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                         IdUser = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id,
                         Hours = Convert.ToInt64(Convert.ToDateTime(s.To).Subtract(Convert.ToDateTime(s.From)).TotalSeconds),
                         Draggable = moveOrResizeScheduled,
                         Resizable = moveOrResizeScheduled,

                     }).ToList();
        }

        public IEnumerable<SchedulingCustomEntity> GetSchedulingByStaff(long id, long period)
        {
                var periodRow = this.GetPeriod(period);
                var moveOrResizeScheduled = periodRow.State != "CL";

                return context.scheduling
                                .Where(c => c.State != "D" && c.IdfAssignedToNavigation.IdfStaff == id && c.IdfPeriod == period)
                         .Select(s => new SchedulingCustomEntity
                         {
                             Id = s.Id,
                             AllDay = s.AllDay,
                             From = s.From,
                             To = s.To,
                             State = s.State,
                             IdDuplicate = s.IdDuplicate,
                             IdfAssignedTo = s.IdfAssignedTo,
                             IdfStaff = s.IdfAssignedToNavigation.IdfStaff,
                    AssignedToPosition = s.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                    ProjectName = s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,
                             ProjectColor = s.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                    AssignedToFullName = string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                             IdUser = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id,
                             Hours = Convert.ToInt64(Convert.ToDateTime(s.To).Subtract(Convert.ToDateTime(s.From)).TotalSeconds),
                             Draggable = moveOrResizeScheduled,
                             Resizable = moveOrResizeScheduled
                         }).ToList();
        }

        public IEnumerable<SchedulingCustomEntity> GetSchedulingByStaff(long idfStaff)
        {
            return context.scheduling
                         .Where(c => c.State != "D" && c.IdfAssignedToNavigation.IdfStaff == idfStaff)
                         .Select(s => new SchedulingCustomEntity
                         {
                             Id = s.Id,
                             AllDay = s.AllDay,
                             From = s.From,
                             To = s.To,
                             State = s.State,
                             IdDuplicate = s.IdDuplicate,
                             IdfAssignedTo = s.IdfAssignedTo,
                             IdfStaff = s.IdfAssignedToNavigation.IdfStaff,
                             AssignedToPosition = s.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                             ProjectName = s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,
                             ProjectColor = s.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                             AssignedToFullName = string.Format("{0} {1}", s .IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                             IdUser = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id,								 
                             Hours = Convert.ToInt64(Convert.ToDateTime(s.To).Subtract(Convert.ToDateTime(s.From)).TotalSeconds)
                         }).ToList();
        }

        private scheduling getScheduled(long period, MySqlContextDB contextAux, long assignedTo, DateTime time1, DateTime time2, long idduplicate)
        {
            var idfProject = contextAux.staff_project_position.Where(c => c.Id == assignedTo).FirstOrDefault().IdfProject;

            var result = new scheduling
            {
                State = "C",
                IdfPeriod = period,
                From = time1,
                To = time2,
                IdfAssignedTo = assignedTo,
                CreationDate = DateTime.Now,
                IdfProject = idfProject
            };

            return result;
        }

        private List<DayOfWeek> DaysOfWeekChecked(duplicate_scheduling d)
        {
            var result = new List<DayOfWeek>();

            if (d.Weekly_Su==1)
            {
                result.Add(DayOfWeek.Sunday);
            }
            if (d.Weekly_Mo==1)
            {
                result.Add(DayOfWeek.Monday);
            }
            if (d.Weekly_Tu==1)
            {
                result.Add(DayOfWeek.Tuesday);
            }
            if (d.Weekly_We==1)
            {
                result.Add(DayOfWeek.Wednesday);
            }
            if (d.Weekly_Th==1)
            {
                result.Add(DayOfWeek.Thursday);
            }
            if (d.Weekly_Fr==1)
            {
                result.Add(DayOfWeek.Friday);
            }
            if (d.Weekly_Sa==1)
            {
                result.Add(DayOfWeek.Saturday);
            }

            return result;
        }

        public CommonResponse SaveScheduling(long period, List<long> StaffsIds, DateTime Date, DateTime Time1, DateTime Time2, duplicate_scheduling DuplicateScheduling)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
                   
            try
            {
                //var date1 = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Hour, Time1.Minute, 0);
                //var date2 = new DateTime(Date.Year, Date.Month, Date.Day, Time2.Hour, Time2.Minute, 0);

                Time1 = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Hour, Time1.Minute, 0);
                Time2 = new DateTime(Date.Year, Date.Month, Date.Day, Time2.Hour, Time2.Minute, 0);


                var date1 = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Hour, Time1.Minute, 0);
                var dateAux = Time1.CompareTo(Time2) > 0 ? Date.AddDays(1) : Date; // time 2 is greather than time 1
                var date2 = new DateTime(dateAux.Year, dateAux.Month, dateAux.Day, Time2.Hour, Time2.Minute, 0);

                var ocurrencesFound = 0;

                // aqui cambie
                if (DuplicateScheduling == null)
                {
                    foreach (var s in StaffsIds)
                    {
                        var newScheduled = this.getScheduled(period, context, s, date1, date2, 0);
                        context.scheduling.Add(newScheduled);
                    }
                }
                else
                {
                    foreach (var s in StaffsIds)
                    {
                        //date1 = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Hour, Time1.Minute, 0);
                        //date2 = new DateTime(Date.Year, Date.Month, Date.Day, Time2.Hour, Time2.Minute, 0);

                        //Time1 = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Hour, Time1.Minute, 0);
                        //Time2 = new DateTime(Date.Year, Date.Month, Date.Day, Time2.Hour, Time2.Minute, 0);

                        date1 = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Hour, Time1.Minute, 0);
                         dateAux = Time1.CompareTo(Time2) > 0 ? Date.AddDays(1) : Date;  // time 2 is greather than time 1
                         date2 = new DateTime(dateAux.Year, dateAux.Month, dateAux.Day, Time2.Hour, Time2.Minute, 0);


                        switch (DuplicateScheduling.DuplicateValue)
                        {
                            case "D":
                                for (int o = 0; o < DuplicateScheduling.EndAfter; o++)
                                {
                                    var newScheduled = this.getScheduled(period, context, s, date1, date2, 0);
                                    context.scheduling.Add(newScheduled);
                                    date1 = date1.AddDays(DuplicateScheduling.RepeatEvery);
                                    date2 = date2.AddDays(DuplicateScheduling.RepeatEvery);
                                }

                                if (DuplicateScheduling.EndAfter == 0)
                                {
                                    do
                                    {
                                        var newScheduled = this.getScheduled(period, context, s, date1, date2, 0);
                                        context.scheduling.Add(newScheduled);
                                        date1 = date1.AddDays(DuplicateScheduling.RepeatEvery);
                                        date2 = date2.AddDays(DuplicateScheduling.RepeatEvery);
                                    }
                                    while (DateTime.Compare(date2, Convert.ToDateTime(DuplicateScheduling.EndOn)) <= 0); // <=
                                   // while (DateTime.Compare(Convert.ToDateTime(DuplicateScheduling.EndOn), date2) > 0); // <=
                                }

                                break;
                            case "W":
                                ocurrencesFound = 0;
                                var daysChecked = this.DaysOfWeekChecked(DuplicateScheduling);


                                DuplicateScheduling.EndAfter = StaffsIds.Count * DuplicateScheduling.EndAfter;

                                do
                                {
                                    for (int w = 0; w < 7; w++)
                                    {
                                        foreach (var d in daysChecked)
                                        {
                                            if (d == date1.DayOfWeek && ((ocurrencesFound < DuplicateScheduling.EndAfter && DuplicateScheduling.EndAfter > 0) || (Convert.ToDateTime(DuplicateScheduling.EndOn).CompareTo(date1) >= 0 && DuplicateScheduling.EndAfter == 0)))
                                            {
                                                var newScheduled = this.getScheduled(period, context, s, date1, date2, 0);
                                                context.scheduling.Add(newScheduled);
                                                ocurrencesFound++;
                                            }
                                        }

                                        date1 = date1.AddDays(1);
                                        date2 = date2.AddDays(1);
                                    }

                                    if (DuplicateScheduling.RepeatEvery > 1)
                                    {
                                        date1 = date1.AddDays(7 * DuplicateScheduling.RepeatEvery);
                                        date2 = date2.AddDays(7 * DuplicateScheduling.RepeatEvery);
                                    }

                                } while ((DuplicateScheduling.EndAfter > 0 && ocurrencesFound < DuplicateScheduling.EndAfter) || (DuplicateScheduling.EndAfter == 0 && Convert.ToDateTime(DuplicateScheduling.EndOn).CompareTo(date1) > 0));

                                break;
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

            return result;
        }

        public CommonResponse UpdateScheduling(long id, DateTime Date, DateTime Time1, DateTime Time2)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
               
            try
            {
                var scheduled = context.scheduling.Where(c => c.Id == id).FirstOrDefault();

                Time1 = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Hour, Time1.Minute, 0);
                Time2 = new DateTime(Date.Year, Date.Month, Date.Day, Time2.Hour, Time2.Minute, 0);

                var date1 = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Hour, Time1.Minute, 0);
                var dateAux = Time1.CompareTo(Time2) > 0 ? Date.AddDays(1) : Date; // time 2 is greather than time 1
                var date2 = new DateTime(dateAux.Year, dateAux.Month, dateAux.Day, Time2.Hour, Time2.Minute, 0);

                // scheduled.From = new DateTime(Date.Year, Date.Month, Date.Day, Time1.Hour, Time1.Minute, 0);
                // scheduled.To = new DateTime(Date.Year, Date.Month, Date.Day, Time2.Hour, Time2.Minute, 0);

                scheduled.From = date1;
                scheduled.To = date2;

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

        public CommonResponse DeleteScheduling(long id)
        {
            var result = new CommonResponse();
          
            var scheduled = context.scheduling.Where(c => c.Id == id).FirstOrDefault();
            scheduled.State = "D";
            context.SaveChanges();
          
            return result;
        }

    }
}