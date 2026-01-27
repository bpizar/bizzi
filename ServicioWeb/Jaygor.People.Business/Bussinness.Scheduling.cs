//-----------------------------------------------------------------------
// <copyright file="Bussinness.Scheduling.cs" company="jaygor">
//     Jaygor copyright 2.
// </copyright>
//-----------------------------------------------------------------------

namespace JayGor.People.Bussinness
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JayGor.People.Entities.CustomEntities;
    using JayGor.People.Entities.Entities;
    using JayGor.People.Entities.Responses;

    /// <summary>
    /// Bussinness layer.
    /// </summary>
    public partial class BussinnessLayer
    {

        public bool DeleteSelectedSchedules(List<long> listaIdSchedules)
        {
            return dataAccessLayer.DeleteSelectedSchedules(listaIdSchedules);
        }

        /// <summary>
        /// Gets the scheduling.
        /// </summary>
        /// <param name="period">The Period.</param>
        /// <param name="scheduling">The Scheduling.</param>
        /// <param name="overTime">Over time.</param>
        /// <param name="overLap">Over lap.</param>
        /// <param name="staff">The Staff.</param>
        /// <param name="projects">The Projects.</param>
        public void GetScheduling(
                            long period,
                            out List<SchedulingCustomEntity> scheduling,
                            out List<OverTimeCustomEntity> overTime,
                            out List<OverLapCustomEntity> overLap,
                            out List<StaffForPlanningCustomEntity> staff,
                            out List<ProjectCustomEntity> projects)
        {
            projects = new List<ProjectCustomEntity>();

            scheduling = dataAccessLayer.GetScheduling(period).ToList();

            var medicalListReminders = dataAccessLayer.GetMedicalRemindersByPeriod(period);

            foreach (var s in medicalListReminders)
            {
                var currentDate = new DateTime(s.From.Year, s.From.Month, s.From.Day, 0, 0, 1);

                do
                {
                    scheduling.Add(new SchedulingCustomEntity
                    {
                        Id = s.Id,
                        AllDay = 1,
                        From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, s.Datetime.Hour, s.Datetime.Minute, 0),
                        To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 23, 59, 59),
                        State = "MM",
                        IdDuplicate = 0,
                        IdfAssignedTo = s.IdfAssignedTo,
                        IdfStaff = 0,
                        AssignedToPosition = string.Empty,
                        ProjectName = s.ProjectName,
                        ProjectColor = s.Color,
                        AssignedToFullName = s.Client,
                        IdUser = s.IdUser,
                        Hours = 0,
                        Resizable = false,
                        Draggable = false,
                        Img = s.Img,
                        subject = s.Description,
                        IdfProject = 0,
                    });

                    currentDate = currentDate.AddDays(1);
                }
                while (new DateTime(s.To.Year, s.To.Month, s.To.Day, 0, 0, 1) != currentDate && new DateTime(s.To.Year, s.To.Month, s.To.Day, 0, 0, 1).CompareTo(currentDate) > 0);
            }

            staff = dataAccessLayer.GetStaffForScheduling(period).ToList();

            staff.ToList().ForEach(c => c.HoursFree = c.Hours - c.HoursAssigned);

            var auxProjects = staff.ToList().DistinctBy(c => c.IdfProject).ToList();

            projects.Add(new ProjectCustomEntity
            {
                Id = 0,
                ProjectName = "No Filter",
            });

            if (auxProjects != null && auxProjects.Count > 0)
            {
                projects.AddRange(auxProjects.Select(
                       p => new ProjectCustomEntity
                       {
                           Id = p.IdfProject,
                           ProjectName = p.ProjectName,
                       })
                       .ToList());
            }

            this.GetOverTimeAndOverLap(period, staff, scheduling, out overTime, out overLap);
        }

        /// <summary>
        /// Gets the scheduling by own projects.
        /// </summary>
        /// <param name="period">The Period.</param>
        /// <param name="username">The Username.</param>
        /// <param name="scheduling">The Scheduling.</param>
        /// <param name="overTime">The Over time.</param>
        /// <param name="overLap">The Over lap.</param>
        /// <param name="staff">The Staff.</param>
        /// <param name="projects">The Projects.</param>
        public void GetSchedulingByOwnProjects(
                        long period,
                        string username,
                        out List<SchedulingCustomEntity> scheduling,
                        out List<OverTimeCustomEntity> overTime,
                        out List<OverLapCustomEntity> overLap,
                        out List<StaffForPlanningCustomEntity> staff,
                        out List<ProjectCustomEntity> projects)
        {
            projects = new List<ProjectCustomEntity>();
            scheduling = dataAccessLayer.GetSchedulingByOwnProjects(period, username).ToList();

            staff = dataAccessLayer.GetStaffByOwnProjects(period, username).ToList();
            staff.ToList().ForEach(c => c.HoursFree = c.Hours - c.HoursAssigned);

            var auxProjects = staff.ToList().DistinctBy(c => c.IdfProject).ToList();

            projects.Add(new ProjectCustomEntity
            {
                Id = 0,
                ProjectName = "No Filter",
            });

            if (auxProjects != null && auxProjects.Count > 0)
            {
                projects.AddRange(auxProjects.Select(
                       p => new ProjectCustomEntity
                       {
                           Id = p.IdfProject,
                           ProjectName = p.ProjectName,
                       })
                       .ToList());
            }

            var medicalListReminders = dataAccessLayer.GetMedicalRemindersByPeriodAndOwnerProjects(period);

            foreach (var s in medicalListReminders)
            {
                var currentDate = new DateTime(s.From.Year, s.From.Month, s.From.Day, 0, 0, 1);

                do
                {
                    scheduling.Add(new SchedulingCustomEntity
                    {
                        Id = s.Id,
                        AllDay = 1,
                        From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, s.Datetime.Hour, s.Datetime.Minute, 0),
                        To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 23, 59, 59),
                        State = "MM",
                        IdDuplicate = 0,
                        IdfAssignedTo = s.IdfAssignedTo,
                        IdfStaff = 0,
                        AssignedToPosition = string.Empty,
                        ProjectName = s.ProjectName,
                        ProjectColor = s.Color,
                        AssignedToFullName = s.Client,
                        IdUser = s.IdUser,
                        Hours = 0,
                        Resizable = false,
                        Draggable = false,
                        Img = s.Img,
                        subject = s.Description,
                        IdfProject = 0
                    });

                    currentDate = currentDate.AddDays(1);
                }
                while (new DateTime(s.To.Year, s.To.Month, s.To.Day, 0, 0, 1) != currentDate && new DateTime(s.To.Year, s.To.Month, s.To.Day, 0, 0, 1).CompareTo(currentDate) > 0);
            }

            this.GetOverTimeAndOverLap(period, staff, scheduling, out overTime, out overLap);

            // return dataAccessLayer.GetSchedulingByOwnProjects(period, username);
        }

        /// <summary>
        /// Gets the scheduling by own scheduling.
        /// </summary>
        /// <param name="period">The Period.</param>
        /// <param name="username">The Username.</param>
        /// <param name="scheduling">The Scheduling.</param>
        /// <param name="overTime">The Over time.</param>
        /// <param name="overLap">The Over lap.</param>
        /// <param name="staff">The Staff.</param>
        /// <param name="projects">The Projects.</param>
        public void GetSchedulingByOwnScheduling(
                                long period,
                                string username,
                                out List<SchedulingCustomEntity> scheduling,
                                out List<OverTimeCustomEntity> overTime,
                                out List<OverLapCustomEntity> overLap,
                                out List<StaffForPlanningCustomEntity> staff,
                                out List<ProjectCustomEntity> projects)
        {
            projects = new List<ProjectCustomEntity>();
            scheduling = dataAccessLayer.GetSchedulingByOwnScheduling(period, username).ToList();
            staff = dataAccessLayer.GetStaffByOwnScheduling(period, username).ToList();
            staff.ToList().ForEach(c => c.HoursFree = c.Hours - c.HoursAssigned);

            var auxProjects = staff.ToList().DistinctBy(c => c.IdfProject).ToList();

            projects.Add(new ProjectCustomEntity
            {
                Id = 0,
                ProjectName = "No Filter",
            });

            if (auxProjects != null && auxProjects.Count > 0)
            {
                projects.AddRange(auxProjects.Select(
                       p => new ProjectCustomEntity
                       {
                           Id = p.IdfProject,
                           ProjectName = p.ProjectName,
                       })
                       .ToList());
            }

            var medicalListReminders = dataAccessLayer.GetMedicalRemindersByPeriodAndAssignedToMe(period, username);

            foreach (var s in medicalListReminders)
            {
                var currentDate = new DateTime(s.From.Year, s.From.Month, s.From.Day, 0, 0, 1);

                do
                {
                    scheduling.Add(new SchedulingCustomEntity
                    {
                        Id = s.Id,
                        AllDay = 1,
                        From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, s.Datetime.Hour, s.Datetime.Minute, 0),
                        To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 23, 59, 59),
                        State = "MM",
                        IdDuplicate = 0,
                        IdfAssignedTo = s.IdfAssignedTo,
                        IdfStaff = 0,
                        AssignedToPosition = string.Empty,
                        ProjectName = s.ProjectName,
                        ProjectColor = s.Color,
                        AssignedToFullName = s.Client,
                        IdUser = s.IdUser,
                        Hours = 0,
                        Resizable = false,
                        Draggable = false,
                        Img = s.Img,
                        subject = s.Description,
                        IdfProject = 0
                    });

                    currentDate = currentDate.AddDays(1);
                }
                while (new DateTime(s.To.Year, s.To.Month, s.To.Day, 0, 0, 1) != currentDate && new DateTime(s.To.Year, s.To.Month, s.To.Day, 0, 0, 1).CompareTo(currentDate) > 0);
            }

            this.GetOverTimeAndOverLap(period, staff, scheduling, out overTime, out overLap);
        }

        /// <summary>
        /// Saves the scheduling.
        /// </summary>
        /// <returns>The scheduling.</returns>
        /// <param name="timeDifference">Time difference.</param>
        /// <param name="period">The Period.</param>
        /// <param name="staffsIds">Staffs identifiers.</param>
        /// <param name="date">The Date.</param>
        /// <param name="time1">The Time1.</param>
        /// <param name="time2">The Time2.</param>
        /// <param name="duplicateScheduling">Duplicate scheduling.</param>
        public CommonResponse SaveScheduling(double timeDifference, long period, List<long> staffsIds, DateTime date, DateTime time1, DateTime time2, duplicate_scheduling duplicateScheduling)
        {
            return dataAccessLayer.SaveScheduling(period, staffsIds, date.AddHours(timeDifference), time1.AddHours(timeDifference), time2.AddHours(timeDifference), duplicateScheduling);
        }

        /// <summary>
        /// Gets the scheduling by staff.
        /// </summary>
        /// <returns>The scheduling by staff.</returns>
        /// <param name="id">The Identifier.</param>
        /// <param name="period">The Period.</param>
        public IEnumerable<SchedulingCustomEntity> GetSchedulingByStaff(long id, long period)
        {
            return dataAccessLayer.GetSchedulingByStaff(id, period);
        }

        /// <summary>
        /// Gets the scheduling by project.
        /// </summary>
        /// <returns>The scheduling by project.</returns>
        /// <param name="project">The Project.</param>
        /// <param name="period">The Period.</param>
        public IEnumerable<SchedulingCustomEntity> GetSchedulingByProject(long project, long period)
        {
            return dataAccessLayer.GetSchedulingByProject(project, period);
        }

        /// <summary>
        /// Gets the scheduling by staff.
        /// </summary>
        /// <returns>The scheduling by staff.</returns>
        /// <param name="idfStaff">The Idf staff.</param>
        public IEnumerable<SchedulingCustomEntity> GetSchedulingByStaff(long idfStaff)
        {
            return dataAccessLayer.GetSchedulingByStaff(idfStaff);
        }

        /// <summary>
        /// Updates the scheduling.
        /// </summary>
        /// <returns>The scheduling.</returns>
        /// <param name="timeDifference">Time difference.</param>
        /// <param name="id">The dentifier.</param>
        /// <param name="date">The Date.</param>
        /// <param name="time1">The Time1.</param>
        /// <param name="time2">The Time2.</param>
        public CommonResponse UpdateScheduling(double timeDifference, long id, DateTime date, DateTime time1, DateTime time2)
        {
            return dataAccessLayer.UpdateScheduling(id, date, time1.AddHours(timeDifference), time2.AddHours(timeDifference));
        }

        /// <summary>
        /// Deletes the scheduling.
        /// </summary>
        /// <returns>The scheduling.</returns>
        /// <param name="id">The Identifier.</param>
        public CommonResponse DeleteScheduling(long id)
        {
            return dataAccessLayer.DeleteScheduling(id);
        }

        /// <summary>
        /// Gets the over time and over lap.
        /// </summary>
        /// <param name="period">The Period.</param>
        /// <param name="staff">The Staff.</param>
        /// <param name="scheduling">The Scheduling.</param>
        /// <param name="overTime">The Over time.</param>
        /// <param name="overLap">The Over lap.</param>
        private void GetOverTimeAndOverLap(
                    long period,
                    List<StaffForPlanningCustomEntity> staff,
                    List<SchedulingCustomEntity> scheduling,
                    out List<OverTimeCustomEntity> overTime,
                    out List<OverLapCustomEntity> overLap)
        {
            overTime = new List<OverTimeCustomEntity>();
            overLap = new List<OverLapCustomEntity>();

            var statusesAppointment = "outOfOffice";

            var completeScheduling = dataAccessLayer.GetScheduling(period).Where(c=> c.State != "MM" && c.State != "TT" && c.State != statusesAppointment).ToList();

            foreach (var sn in staff)
            {
                sn.IdNavAux = -1;
                sn.Nav = new List<long>();
                foreach (var ss in scheduling.Where(c => c.IdfAssignedTo == sn.IdfStaffProjectPosition && c.State != "MM" && c.State != "TT" && c.State != statusesAppointment).OrderBy(c => c.From))
                {
                    sn.Nav.Add(ss.Id);
                }
            }

            foreach (var stf in staff.DistinctBy(c => c.IdfStaff))
            {
                var allowedTotalHours = stf.MaxHoursByPeriod;
                var totalHoursScheduled = staff.Where(c => c.IdfStaff == stf.IdfStaff).Sum(c => c.Hours);

                if (allowedTotalHours < totalHoursScheduled)
                {
                    // Es overtime global
                    var i = 0;

                    foreach (var stf2 in staff.Where(c => c.IdfStaff == stf.IdfStaff).DistinctBy(c => new { c.IdfStaff, c.IdfStaffProjectPosition }).OrderBy(c => c.IdfStaff).OrderBy(c => c.IdfStaff))
                    {
                        var newOverTime = new OverTimeCustomEntity
                        {
                            Color = stf2.Color,
                            Group = string.Format("{0}|{1}|{2}|{3}", stf2.FullUserName, stf2.Img, allowedTotalHours, totalHoursScheduled),
                            Hours = stf2.Hours,
                            PositionName = stf2.PositionName,
                            ProjectName = stf2.ProjectName,
                            Id = i++,
                            Img = stf.Img,
                            HoursFree = stf2.HoursFree,
                            IdNavAux = -1
                        };

                        newOverTime.Nav = new List<long>();

                        foreach (var ss in scheduling.Where(c => c.IdfAssignedTo == stf2.IdfStaffProjectPosition && c.State != "MM" && c.State != "TT" && c.State != statusesAppointment).OrderBy(c => c.From))
                        {
                            newOverTime.Nav.Add(ss.Id);
                        }

                        overTime.Add(newOverTime);
                        scheduling.Where(c => c.IdfAssignedTo == stf2.IdfStaffProjectPosition && c.State != "MM" && c.State != "TT" && c.State != statusesAppointment).ToList().ForEach(s => { s.IsDirty = true; s.State = statusesAppointment; });
                    }
                }
                else
                {
                    // no es tvertime global
                    var i = 0;

                    foreach (var stf3 in staff.Where(c => c.IdfStaff == stf.IdfStaff).DistinctBy(c => new { c.IdfStaff, c.IdfStaffProjectPosition }).OrderBy(c => c.IdfStaff).OrderBy(c => c.IdfStaff))
                    {
                        if (stf3.Hours != 0 && stf3.Hours < stf3.HoursAssigned)
                        {
                            var newOverTime2 = new OverTimeCustomEntity
                            {
                                Color = stf3.Color,
                                Group = string.Format("{0}|{1}|{2}|{3}", stf3.FullUserName, stf3.Img, allowedTotalHours, totalHoursScheduled),
                                Hours = stf3.Hours,
                                PositionName = stf3.PositionName,
                                ProjectName = stf3.ProjectName,
                                Id = i++,
                                Img = stf.Img,
                                HoursFree = stf3.HoursFree,
                                IdNavAux = -1
                            };

                            overTime.Add(newOverTime2);

                            newOverTime2.Nav = new List<long>();

                            foreach (var ss in scheduling.Where(c => c.IdfAssignedTo == stf3.IdfStaffProjectPosition && c.State != "MM" && c.State != "TT" && c.State != statusesAppointment).OrderBy(c => c.From))
                            {
                                newOverTime2.Nav.Add(ss.Id);
                            }

                            scheduling.Where(c => c.IdfAssignedTo == stf3.IdfStaffProjectPosition && c.State != "MM" && c.State != "TT" && c.State != statusesAppointment).ToList().ForEach(s => { s.IsDirty = true; s.State = statusesAppointment; });
                        }
                    }
                }

                // OVERL
                foreach (var s in scheduling.Where(c => c.IdfStaff == stf.IdfStaff && c.State != "MM" && c.State != "TT" && c.State != statusesAppointment).OrderBy(c => c.From))
                {
                    foreach (var cs in completeScheduling.OrderBy(c => c.From))
                    {
                        if (s.Id != cs.Id && s.IdfStaff == cs.IdfStaff)
                        {
                            var sf = Convert.ToDateTime(s.From);
                            var st = Convert.ToDateTime(s.To);

                            var csf = Convert.ToDateTime(cs.From);
                            var cst = Convert.ToDateTime(cs.To);

                            if ((DateTime.Compare(sf, csf) >= 0 && DateTime.Compare(sf, cst) < 0) || (DateTime.Compare(st, csf) > 0 && DateTime.Compare(st, cst) <= 0))
                            {
                                if (!overLap.Any(c => c.Id == s.Id))
                                {
                                    overLap.Add(new OverLapCustomEntity
                                    {
                                        Color = s.ProjectColor,
                                        Group = string.Format("{0}|{1}", s.AssignedToFullName, s.Img),

                                        // Hours = stf2.Hours,
                                        PositionName = s.AssignedToPosition,
                                        From = Convert.ToDateTime(s.From).ToString("HH:mm:ss"),
                                        To = Convert.ToDateTime(s.To).ToString("HH:mm:ss"),
                                        Id = s.Id,
                                        IdTask = 0, // deprecar
                                        ProjectName = s.ProjectName,
                                        IdNavAux = s.Id
                                    });

                                    // set dirty
                                    s.IsDirty = true;
                                    s.State = statusesAppointment;
                                }

                                if (!overLap.Any(c => c.Id == cs.Id))
                                {
                                    overLap.Add(new OverLapCustomEntity
                                    {
                                        Color = cs.ProjectColor,
                                        Group = string.Format("{0}|{1}", cs.AssignedToFullName, cs.Img),
                                        PositionName = cs.AssignedToPosition + " " + cs.Id,
                                        From = Convert.ToDateTime(cs.From).ToString("HH:mm:ss"),
                                        To = Convert.ToDateTime(cs.To).ToString("HH:mm:ss"),
                                        Id = cs.Id,
                                        IdTask = 0, // deprecar
                                        ProjectName = cs.ProjectName,
                                        IdNavAux = cs.Id
                                    });

                                    cs.IsDirty = true;
                                    cs.State = statusesAppointment;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}