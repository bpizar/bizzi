using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.DataAccess.MySql;
//using JayGor.People.DataAccess.MySql;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        private List<DayOfWeek> DaysOfWeekChecked(duplicate_tasks d)
        {
            var result = new List<DayOfWeek>();

            if (d.Weekly_Su==1)
            {
                result.Add(DayOfWeek.Sunday);
            }
            if (d.Weekly_Mo== 1)
            {
                result.Add(DayOfWeek.Monday);
            }
            if (d.Weekly_Tu== 1)
            {
                result.Add(DayOfWeek.Tuesday);
            }
            if (d.Weekly_We== 1)
            {
                result.Add(DayOfWeek.Wednesday);
            }
            if (d.Weekly_Th== 1)
            {
                result.Add(DayOfWeek.Thursday);
            }
            if (d.Weekly_Fr== 1)
            {
                result.Add(DayOfWeek.Friday);
            }
            if (d.Weekly_Sa== 1)
            {
                result.Add(DayOfWeek.Saturday);
            }

            return result;
        } 

        //Fix id issue on mapper  
        private duplicate_tasks getDuplicate(duplicate_tasks duplicateTask)
        {
           return new duplicate_tasks
            {
                DuplicateValue = duplicateTask.DuplicateValue,
                RepeatEvery = duplicateTask.RepeatEvery,
                Weekly_Su = duplicateTask.Weekly_Su,
                Weekly_Mo = duplicateTask.Weekly_Mo,
                Weekly_Tu = duplicateTask.Weekly_Tu,
                Weekly_We = duplicateTask.Weekly_We,
                Weekly_Th = duplicateTask.Weekly_Th,
                Weekly_Fr = duplicateTask.Weekly_Fr,
                Weekly_Sa = duplicateTask.Weekly_Sa,
                Monthly_Day = duplicateTask.Monthly_Day,
                Yearly_Month = duplicateTask.Yearly_Month,
                Yearly_MonthDay = duplicateTask.Yearly_MonthDay,
                EndAfter = duplicateTask.EndAfter,
                EndOn = duplicateTask.EndOn
            };
        }

        //Fix id issue on mapper    
        private tasks getTask(MySqlContextDB context,tasks t, DateTime date1, DateTime date2, long idduplicate)
        {
            var idfProject = context.staff_project_position.Where(c => c.Id == t.IdfAssignedTo).FirstOrDefault().IdfProject;

            var result =  new tasks
            {
                IdfTaskParent = t.IdfTaskParent,
                Subject = t.Subject,
                State = "C",
                 Type= t.Type,
                Deadline = t.Deadline,
                IdfAssignedTo = t.IdfAssignedTo,
                CreationDate = DateTime.Now,
                Lat = t.Lat,
                Lon = t.Lon,
                Address = t.Address,
                IdfProject = idfProject
            };

            if (idduplicate > 0)
            {
                result.IdDuplicate = idduplicate;
            }

            return result;
        }

        private long SaveDuplicate(MySqlContextDB context, ref tasks task, ref duplicate_tasks duplicateTask, ref DateTime date1, ref DateTime date2)
        {
            long idduplicate = 0;

            var newDuplicate = this.getDuplicate(duplicateTask);
            context.duplicate_tasks.Add(newDuplicate);
            context.SaveChanges();
            idduplicate = newDuplicate.Id;

            var ocurrencesFound = 0;

            switch (duplicateTask.DuplicateValue)
            {
                case "D":
                    for (int o = 0; o < duplicateTask.EndAfter; o++)
                    {
                        var newTask = this.getTask(context,task, date1, date2, idduplicate);
                        context.tasks.Add(newTask);
                        date1 = date1.AddDays(duplicateTask.RepeatEvery);
                        date2 = date2.AddDays(duplicateTask.RepeatEvery);
                    }

                    if (duplicateTask.EndAfter == 0)
                    {
                        do
                        {
                            var newTask = this.getTask(context,task, date1, date2, idduplicate);
                            context.tasks.Add(newTask);
                            date1 = date1.AddDays(duplicateTask.RepeatEvery);
                            date2 = date2.AddDays(duplicateTask.RepeatEvery);
                        }
                        while (DateTime.Compare(date2, Convert.ToDateTime(duplicateTask.EndOn)) <= 0);
                    }

                    break;
                case "W":
                    ocurrencesFound = 0;
                    var daysChecked = this.DaysOfWeekChecked(duplicateTask);

                    do
                    {
                        for (int w = 0; w < 7; w++)
                        {
                            foreach (var d in daysChecked)
                            {
                                if (d == date1.DayOfWeek && ((ocurrencesFound < duplicateTask.EndAfter && duplicateTask.EndAfter > 0) || (Convert.ToDateTime(duplicateTask.EndOn).CompareTo(date1) >= 0 && duplicateTask.EndAfter == 0)))
                                {
                                    var newTask = this.getTask(context, task, date1, date2, idduplicate);
                                    context.tasks.Add(newTask);
                                    ocurrencesFound++;
                                }
                            }

                            date1 = date1.AddDays(1);
                            date2 = date2.AddDays(1);
                        }

                        if (duplicateTask.RepeatEvery > 1)
                        {
                            date1 = date1.AddDays(7 * duplicateTask.RepeatEvery);
                            date2 = date2.AddDays(7 * duplicateTask.RepeatEvery);
                        }

                    } while ((duplicateTask.EndAfter > 0 && ocurrencesFound < duplicateTask.EndAfter) || (duplicateTask.EndAfter == 0 && Convert.ToDateTime(duplicateTask.EndOn).CompareTo(date1) > 0));


                    break;
                case "M":
                    ocurrencesFound = 0;
                    var day = (int)duplicateTask.Monthly_Day;
                    var initialDate = new DateTime(date1.Year, date1.Month, day, date1.Hour, date1.Minute, date1.Second, 0);

                    var originalDay = date1.Day;

                    var differenceDay = day - originalDay;

                    if (differenceDay > 0)
                    {
                        date1 = date1.AddDays(Math.Abs(differenceDay));
                        date2 = date2.AddDays(Math.Abs(differenceDay));
                    }
                    else if (differenceDay < 0)
                    {
                        date1 = date1.AddDays(Math.Abs(differenceDay));
                        date2 = date2.AddDays(Math.Abs(differenceDay));
                        date1 = date1.AddMonths(duplicateTask.RepeatEvery);
                        date2 = date2.AddMonths(duplicateTask.RepeatEvery);
                    }
                    else if (differenceDay == 0)
                    {
                        //todo: nothing
                    }

                    do
                    {
                        if (((ocurrencesFound < duplicateTask.EndAfter && duplicateTask.EndAfter > 0) || (Convert.ToDateTime(duplicateTask.EndOn).CompareTo(date1) >= 0 && duplicateTask.EndAfter == 0)))
                        {
                            var newTask = this.getTask(context, task, date1, date2, idduplicate);
                            context.tasks.Add(newTask);
                            ocurrencesFound++;
                        }
                        date1 = date1.AddMonths(duplicateTask.RepeatEvery);
                        date2 = date2.AddMonths(duplicateTask.RepeatEvery);
                    } while ((duplicateTask.EndAfter > 0 && ocurrencesFound < duplicateTask.EndAfter) || (duplicateTask.EndAfter == 0 && Convert.ToDateTime(duplicateTask.EndOn).CompareTo(date1) > 0));

                    break;
                case "Y":
                    var dayYearly = (int)duplicateTask.Yearly_MonthDay;
                    var monthYearly = (int)duplicateTask.Yearly_Month + 1;

                    var initialDateYearly = new DateTime(date1.Year, monthYearly, dayYearly, date1.Hour, date1.Minute, date1.Second, 0);

                    if (initialDateYearly.CompareTo(date1) > 0)
                    {
                        date1 = initialDateYearly;
                        date2 = new DateTime(date2.Year, monthYearly, dayYearly, date2.Hour, date2.Minute, date2.Second, 0);
                    }
                    else if (initialDateYearly.CompareTo(date1) < 0)
                    {
                        date1 = initialDateYearly;
                        date2 = new DateTime(date2.Year, monthYearly, dayYearly, date2.Hour, date2.Minute, date2.Second, 0);
                        date1 = date1.AddYears(duplicateTask.RepeatEvery);
                        date2 = date2.AddYears(duplicateTask.RepeatEvery);
                    }
                    else if (initialDateYearly.CompareTo(date1) == 0)
                    {
                        //todo: nothing
                    }
                    do
                    {
                        if (((ocurrencesFound < duplicateTask.EndAfter && duplicateTask.EndAfter > 0) || (Convert.ToDateTime(duplicateTask.EndOn).CompareTo(date1) >= 0 && duplicateTask.EndAfter == 0)))
                        {
                            var newTask = this.getTask(context, task, date1, date2, idduplicate);
                            context.tasks.Add(newTask);
                            ocurrencesFound++;
                        }
                        date1 = date1.AddYears(duplicateTask.RepeatEvery);
                        date2 = date2.AddYears(duplicateTask.RepeatEvery);
                    } while ((duplicateTask.EndAfter > 0 && ocurrencesFound < duplicateTask.EndAfter) || (duplicateTask.EndAfter == 0 && Convert.ToDateTime(duplicateTask.EndOn).CompareTo(date1) > 0));

                    break;
            }

            return idduplicate;
        }

        public bool MoveCopyTask(tasks task, bool move, long idfProject, long idfPeriod) //,long IdfProject, long IdfPeriod,bool move)
        {          
            var transaction = context.Database.BeginTransaction();

            try
            {
                if(move)
                {
                    var editTask = context.tasks.Where(c => c.Id == task.Id).FirstOrDefault();

                    if(editTask!=null)
                    {
                        editTask.State = "D";
                        context.SaveChanges();
                    }

                }

                var tasksreminders = context.tasks_reminders.Where(c => c.IdfTask == task.Id && c.IdfPeriod == task.IdfPeriod);
                
                var newTask = new tasks();

                    newTask.IdfTaskParent = task.IdfTaskParent;
                    newTask.Subject= task.Subject;
                    newTask.IdfStatus= task.IdfStatus;
                    newTask.State = "C";//task.State;
                    newTask.Description= task.Description;
                    newTask.Deadline = task.Deadline;                 
                    newTask.RecurrencePattern = task.RecurrencePattern;
                    newTask.RecurrenceException = task.RecurrenceException;
                    newTask.AllDay = task.AllDay;
                    newTask.IdfCreatedBy = task.IdfCreatedBy;
                    newTask.CreationDate = DateTime.Now;
                    newTask.Lat = task.Lat;
                    newTask.Lon = task.Lon;
                    newTask.Address = task.Address;
                    newTask.IdDuplicate = task.IdDuplicate;
                    newTask.IdfProject = idfProject;// task.IdfProject;
                    newTask.Hours = task.Hours;
                    newTask.IdfPeriod = idfPeriod; // task.IdfPeriod;                 
                    newTask.Type = task.Type;
                    newTask.IdfAssignedTo = task.IdfAssignedTo <=0 ? null : task.IdfAssignedTo;
                    newTask.IdfAssignableRol = task.IdfAssignableRol <= 0 ? null : task.IdfAssignableRol;
        
                    context.tasks.Add(newTask);
                    context.SaveChanges();

                    foreach (var x in tasksreminders)
                    {
                        var newTaskRem = new tasks_reminders
                        {
                            IdfTask = newTask.Id,
                            IdfPeriod = idfPeriod,
                            State = x.State,
                            IdfSettingReminderTime = x.IdfSettingReminderTime
                        };

                        context.tasks_reminders.Add(newTaskRem);
                    }
              
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        //TODO: DEPRECATED.
        public bool SaveTask(tasks task, duplicate_tasks duplicateTask, Boolean editingSerie)
        {
            //var context = MySqlServerFactoryDB.Create();
            //var transaction = context.Database.BeginTransaction();

            //try
            //{
            //    var date1 = Convert.ToDateTime(task.From);
            //    var date2 = Convert.ToDateTime(task.To);

            //    var isnew = task.Id == -1;

            //    if (isnew)
            //    {                           
            //        if (duplicateTask != null)
            //        {
            //            this.SaveDuplicate(context, ref task, ref duplicateTask, ref date1, ref date2);
            //        }
            //        else
            //        {
            //            // It is not duplicate.
            //            var newTask = this.getTask(context, task, date1, date2, 0);
            //            context.Tasks.Add(newTask);
            //        }

            //        context.SaveChanges();
            //    }
            //    else {
            //        //IsEdit
            //        if (!editingSerie)
            //        {
            //            var editTask = context.Tasks.Where(c => c.Id == task.Id).FirstOrDefault();

            //            editTask.Subject = task.Subject;
            //            editTask.From = task.From;
            //            editTask.To = task.To;
            //            editTask.Address = task.Address;
            //            editTask.Lat = task.Lon;
            //            editTask.Lon = task.Lon;
            //            editTask.IdfAssignedTo = task.IdfAssignedTo;
            //            editTask.IdfProject = context.Staff_Project_Position.Where(c => c.Id == task.IdfAssignedTo).FirstOrDefault().IdfProject;


            //            if (duplicateTask != null)
            //            {
            //                var idDuplicate = this.SaveDuplicate(context, ref task, ref duplicateTask, ref date1, ref date2);
            //                editTask.IdDuplicate = idDuplicate;                                    
            //            }
            //        }
            //        else {                        
            //            var taskForSerie = context.Tasks.Where(c => c.IdDuplicate == task.IdDuplicate);

            //            foreach (var t in taskForSerie)
            //            {                            
            //                t.Subject = task.Subject;
            //                t.Address = task.Address;
            //                t.Lon = task.Lon;
            //                t.Lat = task.Lat;
            //                t.IdfAssignedTo = task.IdfAssignedTo;
            //                t.IdfProject = context.Staff_Project_Position.Where(c => c.Id == t.IdfAssignedTo).FirstOrDefault().IdfProject;
            //            }
            //        }

            //        context.SaveChanges();
            //    }

            //    transaction.Commit();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    transaction.Rollback();
            //    throw ex;
            //}

            return false;
        }

        public List<tasks_reminders> GetTaskRemindes(long idperiod)
        {
            return context.tasks_reminders
                                    .Where(c => c.IdfPeriod == idperiod && c.State != "D")
                                    .Select(s => new tasks_reminders
                                    {
                                        Id = s.Id,
                                        IdfPeriod = s.IdfPeriod,
                                        IdfSettingReminderTime = s.IdfSettingReminderTime,
                                        IdfTask = s.IdfTask,
                                        State = s.State
                                    }).ToList();
        }

        public tasks GetTask(long id)
        {
           return context.tasks.Where(c => c.Id == id && c.State != "D").FirstOrDefault();
        }

        public duplicate_tasks GetDuplicate(long id)
        {
            return context.duplicate_tasks.Where(c => c.Id == id).FirstOrDefault(); ;                
        }

        //TODO: DEPRECATED.
        public IEnumerable<TasksForPlanningCustomEntity> GetTasks(DateTime from, DateTime to)
        {
            //var context = MySqlServerFactoryDB.Create();
            //var transaction = context.Database.BeginTransaction();

            //try
            //{
            //    var result = context.Tasks
            //             .Where(c=>c.Status != "D" && c.IdfAssignedTo>0 && c.From!=null && c.To !=null )   
            //             .Select(s => new TasksForPlanningCustomEntity
            //             {
            //                 Id = s.Id,
            //                 Address = s.Address,
            //                 AllDay = s.AllDay,
            //                 Description = s.Description,
            //                 From = s.From,
            //                 To = s.To,
            //                 Status = s.Status,
            //                 Lat = s.Lat,
            //                 Lon = s.Lon,
            //                 IdDuplicate = s.IdDuplicate,
            //                 IdfAssignedTo = s.IdfAssignedTo,
            //                 RecurrencePattern = s.RecurrencePattern,
            //                 RecurrenceException = s.RecurrenceException,
            //                 Subject = s.Subject,
            //                 IdfStaff = s.STAFFPROJECTPOSITION.IdfStaff,
            //                 AssignedToPosition = s.STAFFPROJECTPOSITION.POSITION.Name,
            //                 ProjectName = s.STAFFPROJECTPOSITION.PROJECT.ProjectName, //s.STAFFPROJECTPOSITION.PROJECT.Name,
            //                 ProjectColor = s.STAFFPROJECTPOSITION.PROJECT.Color, // s.STAFFPROJECTPOSITION.PROJECT.Color,
            //                 AssignedToColor = s.STAFFPROJECTPOSITION.STAFF.Color,
            //                 AssignedToFullName = string.Format("{0} {1}", s.STAFFPROJECTPOSITION.STAFF.USER.LastName, s.STAFFPROJECTPOSITION.STAFF.USER.FirstName) ,
            //                 IdUser = s.STAFFPROJECTPOSITION.STAFF.USER.Id
            //             }).ToList();

            //    transaction.Commit();
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    transaction.Rollback();
            //    throw ex;
            //}
            return null;
        }

        //TODO: DEPRECATED.
        public IEnumerable<TasksForPlanningCustomEntity> GetUnasignedTasks(DateTime from, DateTime to)
        {
            //var context = MySqlServerFactoryDB.Create();
            //var transaction = context.Database.BeginTransaction();

            //try
            //{
            //    var result = context.Tasks
            //             .Where(c => c.Status != "D" && c.IdfAssignedTo <= 0)
            //             .Select(s => new TasksForPlanningCustomEntity
            //             {
            //                 Id = s.Id,
            //                 Address = s.Address,
            //                 AllDay = s.AllDay,
            //                 Description = s.Description,
            //                 From = s.From,
            //                 To = s.To,
            //                 Status = s.Status,
            //                 Lat = s.Lat,
            //                 Lon = s.Lon,
            //                 IdDuplicate = s.IdDuplicate,
            //                 IdfAssignedTo = 0,
            //                 RecurrencePattern = string.Empty,
            //                 RecurrenceException = string.Empty,
            //                 Subject = s.Subject,
            //                 IdfStaff = 0,
            //                 AssignedToPosition = string.Empty,
            //                 ProjectName = string.Empty,// context.Projects.Where(c=>c.Id == s.IdfProject).FirstOrDefault().ProjectName, //s.STAFFPROJECTPOSITION.PROJECT.ProjectName, 
            //                 ProjectColor = string.Empty,// context.Projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().Color,// .s.STAFFPROJECTPOSITION.PROJECT.Color, 
            //                 AssignedToColor = string.Empty,// s.STAFFPROJECTPOSITION.STAFF.Color,
            //                 AssignedToFullName = "unAssigned",
            //                 IdUser = -10,
            //                 Group = string.Format("#{0}|{1}", context.Projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().Color, context.Projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName)
            //             }).ToList();

            //    result.AddRange(context.Tasks
            //                         .Where(c => c.Status != "D" && c.IdfAssignedTo > 0 && (c.From == null || c.To == null))
            //                         .Select(s => new TasksForPlanningCustomEntity
            //                         {
            //                             Id = s.Id,
            //                             Address = s.Address,
            //                             AllDay = s.AllDay,
            //                             Description = s.Description,
            //                             From = s.From,
            //                             To = s.To,
            //                             Status = s.Status,
            //                             Lat = s.Lat,
            //                             Lon = s.Lon,
            //                             IdDuplicate = s.IdDuplicate,
            //                             IdfAssignedTo = s.IdfAssignedTo,
            //                             RecurrencePattern = s.RecurrencePattern,
            //                             RecurrenceException = s.RecurrenceException,
            //                             Subject = s.Subject,
            //                             IdfStaff = s.STAFFPROJECTPOSITION.IdfStaff,
            //                             AssignedToPosition = s.STAFFPROJECTPOSITION.POSITION.Name,
            //                             ProjectName = s.STAFFPROJECTPOSITION.PROJECT.ProjectName, //s.STAFFPROJECTPOSITION.PROJECT.Name,
            //                             ProjectColor = s.STAFFPROJECTPOSITION.PROJECT.Color, // s.STAFFPROJECTPOSITION.PROJECT.Color,
            //                             AssignedToColor = s.STAFFPROJECTPOSITION.STAFF.Color,
            //                             AssignedToFullName = string.Format("{0} {1}", s.STAFFPROJECTPOSITION.STAFF.USER.LastName, s.STAFFPROJECTPOSITION.STAFF.USER.FirstName),
            //                             IdUser = s.STAFFPROJECTPOSITION.STAFF.USER.Id,
            //                             Group = string.Format("#{0}|{1}", context.Projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().Color, context.Projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName)
            //                         }).ToList());

            //    transaction.Commit();
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    transaction.Rollback();
            //    throw ex;
            //}

            return null;
        }

        public IEnumerable<TasksForPlanningCustomEntity> GetTasksByStaff(long id, long idperiod)
        {
           return context.tasks
                                    .Where(c => c.IdfAssignedTo == c.IdfAssignedToNavigation.Id  &&
                                           c.IdfAssignedToNavigation.IdfStaff == id &&
                                      c.State != "D" &
                                      c.IdfAssignedToNavigation.State != "D" &&
                                      c.IdfPeriod == idperiod)
                            .Select(s => new TasksForPlanningCustomEntity
                            {
                                Id = s.Id,
                                IdfTaskParent = s.IdfTaskParent,
                                Address = s.Address,
                                AllDay = s.AllDay,
                                Description = s.Description,
                                Type= s.Type,
                                Deadline = s.Deadline,
                                State = s.State,
                                Lat = s.Lat,
                                Lon = s.Lon,
                                IdDuplicate = s.IdDuplicate,
                                IdfAssignedTo = s.IdfAssignedTo,
                                RecurrencePattern = s.RecurrencePattern,
                                RecurrenceException = s.RecurrenceException,
                                Subject = s.Subject,
                                IdfStaff = s.IdfAssignedToNavigation.IdfStaff,
                        AssignedToPosition = s.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                        ProjectName = s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName,
                                ProjectColor = s.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                                AssignedToColor = string.Empty,
                        AssignedToFullName = string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                IdUser = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id,
                                Abm = string.Empty,
                                Hours = s.Hours,
                        Group = string.Format("{0}|{1}", s.IdfAssignedToNavigation.IdfProjectNavigation.Color, s.IdfAssignedToNavigation.IdfProjectNavigation.ProjectName)
                            }).ToList();               
        }

        public IEnumerable<TasksForPlanningCustomEntity> GetTasksByProject(long idProject)
        {           
            var result = new List<TasksForPlanningCustomEntity>();
                           
            result = context.tasks
                     .Where(c => c.IdfProject == idProject && c.IdfAssignedTo > 0 && c.State != "D")
                     .Select(s => new TasksForPlanningCustomEntity
                     {
                         Id = s.Id,
                         IdfTaskParent=s.IdfTaskParent,
                         Address = s.Address,
                         AllDay = s.AllDay,
                         Description = s.Description,
                         Type= s.Type,
                         Deadline = s.Deadline,
                         State = s.State,
                         Lat = s.Lat,
                         Lon = s.Lon,
                         IdDuplicate = s.IdDuplicate,
                         IdfAssignedTo = s.IdfAssignedTo,
                         RecurrencePattern = s.RecurrencePattern,
                         RecurrenceException = s.RecurrenceException,
                         Subject = s.Subject,
                         //IdfStaff = s.STAFFPROJECTPOSITION.IdfStaff,
                         IdfStaff = s.IdfAssignedToNavigation.IdfStaff,
                         AssignedToPosition = s.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                         ProjectName = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName,
                         ProjectColor = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().Color,
                         AssignedToColor = s.IdfAssignedToNavigation.IdfStaffNavigation.Color,
                AssignedToFullName = string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                         IdUser = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id,
                         Abm = string.Empty,
                         Hours = s.Hours
                     }).ToList();

            result.AddRange(context.tasks.Where(c => c.IdfProject == idProject && c.IdfAssignedTo == 0 && c.State != "D")
                     .Select(s => new TasksForPlanningCustomEntity
                     {
                         Id = s.Id,
                         IdfTaskParent = s.IdfTaskParent,
                         Address = s.Address,
                         AllDay = s.AllDay,
                         Description = s.Description,
                         Deadline = s.Deadline,
                         State = s.State,
                         Lat = s.Lat,
                         Lon = s.Lon,
                         IdDuplicate = s.IdDuplicate,
                         IdfAssignedTo = s.IdfAssignedTo,
                         RecurrencePattern = s.RecurrencePattern,
                         RecurrenceException = s.RecurrenceException,
                         Subject = s.Subject,
                         IdfStaff = -1,
                         AssignedToPosition = string.Empty,
                         ProjectName = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName,
                         ProjectColor = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().Color,
                         AssignedToColor = string.Empty,
                         AssignedToFullName = "Unassigned",
                         IdUser = -1,
                         Abm = string.Empty,
                         Hours = s.Hours
                     }).ToList());                

            return result;
        }


        public IEnumerable<TasksForPlanningCustomEntity> GetOverdueTasks()
        {           
            var result = new List<TasksForPlanningCustomEntity>();

            result = context.tasks
                     .Where(c => c.IdfAssignedTo > 0 && c.State != "D" && c.Deadline < DateTime.Now && c.IdfStatus != 3 && c.IdfStatus != 4)
                     .Select(s => new TasksForPlanningCustomEntity
                     {
                         Id = s.Id,
                         IdfTaskParent = s.IdfTaskParent,
                         IdfStatus = s.IdfStatus,
                         Status= context.statuses.Where(c => c.Id == s.IdfStatus).FirstOrDefault().status,
                         Address = s.Address,
                         AllDay = s.AllDay,
                         Description = s.Description,
                         Type = s.Type,
                         Deadline = s.Deadline,
                         State = s.State,
                         Lat = s.Lat,
                         Lon = s.Lon,
                         IdDuplicate = s.IdDuplicate,
                         IdfAssignedTo = s.IdfAssignedTo,
                         RecurrencePattern = s.RecurrencePattern,
                         RecurrenceException = s.RecurrenceException,
                         Subject = s.Subject,
                IdfStaff = s.IdfAssignedToNavigation.IdfStaff,
                AssignedToPosition = s.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                         ProjectName = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName,
                         ProjectColor = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().Color,
                AssignedToColor = s.IdfAssignedToNavigation.IdfStaffNavigation.Color,
                AssignedToFullName = string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                         IdUser = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id,
                         Abm = string.Empty,
                         Hours = s.Hours
                     }).ToList();

            result.AddRange(context.tasks.Where(c => c.IdfAssignedTo == 0 && c.State != "D" && c.Deadline < DateTime.Now && c.IdfStatus != 3 && c.IdfStatus != 4)
                     .Select(s => new TasksForPlanningCustomEntity
                     {
                         Id = s.Id,
                         IdfTaskParent = s.IdfTaskParent,
                         IdfStatus = s.IdfStatus,
                         Status = context.statuses.Where(c => c.Id == s.IdfStatus).FirstOrDefault().status,
                         Address = s.Address,
                         AllDay = s.AllDay,
                         Description = s.Description,
                         Deadline = s.Deadline,
                         State = s.State,
                         Lat = s.Lat,
                         Lon = s.Lon,
                         IdDuplicate = s.IdDuplicate,
                         IdfAssignedTo = s.IdfAssignedTo,
                         RecurrencePattern = s.RecurrencePattern,
                         RecurrenceException = s.RecurrenceException,
                         Subject = s.Subject,
                         IdfStaff = -1,
                         AssignedToPosition = string.Empty,
                         ProjectName = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName,
                         ProjectColor = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().Color,
                         AssignedToColor = string.Empty,
                         AssignedToFullName = "Unassigned",
                         IdUser = -1,
                         Abm = string.Empty,
                         Hours = s.Hours
                     }).ToList());
       

            return result;
        }

        public IEnumerable<TasksForPlanningCustomEntity> GetTasksByProject2(long idProject, long idperiod)
        {
               var result = new List<TasksForPlanningCustomEntity>();
                     
               result = context.tasks
                               .Where(c => c.IdfProject == idProject && c.IdfAssignedTo !=null && c.IdfAssignableRol == null && c.State != "D" && c.IdfPeriod == idperiod)
                        .Select(s => new TasksForPlanningCustomEntity
                        {
                            Id = s.Id,
                            IdfTaskParent=s.IdfTaskParent,
                            IdfStatus=s.IdfStatus,
                            Address = s.Address,
                            Status= context.statuses.Where(c => c.Id == s.IdfStatus).FirstOrDefault().status,
                            AllDay = s.AllDay,
                            Description = s.Description,
                            Type= s.Type,
                            Deadline = s.Deadline,
                            State = s.State,
                            Lat = s.Lat,
                            Lon = s.Lon,
                            IdDuplicate = s.IdDuplicate,
                            IdfAssignedTo = s.IdfAssignedTo,
                            RecurrencePattern = s.RecurrencePattern,
                            RecurrenceException = s.RecurrenceException,
                            Subject = s.Subject,
                            IdfStaff = s.IdfAssignedToNavigation.IdfStaff,
                            AssignedToPosition = s.IdfAssignedToNavigation.IdfPositionNavigation.Name,
                            ProjectName = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName,
                            ProjectColor = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().Color,
                            AssignedToColor = s.IdfAssignedToNavigation.IdfStaffNavigation.Color,
                            AssignedToFullName = string.Format("{0} {1}", s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName),
                            IdUser = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.Id,
                            Abm = string.Empty,
                            Hours = s.Hours * 3600,
                            IdfPeriod = s.IdfPeriod,
                            IdfAssignableRol = s.IdfAssignableRol,
                            Img = s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name,
                            Notes = s.Notes

                        }).ToList();

                 result.AddRange(context.tasks.Where(c => c.IdfProject == idProject && c.IdfAssignedTo == null && c.IdfAssignableRol != null && c.State != "D" && c.IdfPeriod == idperiod && c.IdfAssignableRol != null)
                        .Select(s => new TasksForPlanningCustomEntity
                        {
                            Id = s.Id,
                            IdfTaskParent = s.IdfTaskParent,
                            IdfStatus = s.IdfStatus,
                            Address = s.Address,
                            Status = context.statuses.Where(c => c.Id == s.IdfStatus).FirstOrDefault().status,
                            AllDay = s.AllDay,
                            Description = s.Description,
                            Type= s.Type,
                            Deadline = s.Deadline,
                            State = s.State,
                            Lat = s.Lat,
                            Lon = s.Lon,
                            IdDuplicate = s.IdDuplicate,
                            IdfAssignedTo = s.IdfAssignedTo,
                            RecurrencePattern = s.RecurrencePattern,
                            RecurrenceException = s.RecurrenceException,
                            Subject = s.Subject,
                            IdfStaff = -1,
                            AssignedToPosition = string.Empty,
                            ProjectName = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName,
                            ProjectColor = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().Color,
                            AssignedToColor = string.Empty,
                            //AssignedToFullName = string.Format("Rol: {0}", context.Identity_Roles.Where(c=>c.Id == s.IdfAssignableRol && c.State!="D").Single().DisplayShortName),
                            //AssignedToFullName = context.,
                            AssignedToFullName = string.Format("Position: {0}", s.IdfAssignableRolNavigation.Name ),
                            IdUser = -1,
                            Abm = string.Empty,
                            Hours = s.Hours * 3600,
                            IdfPeriod = s.IdfPeriod,
                            IdfAssignableRol = s.IdfAssignableRol,
                            Img = "genericEmploye",
                            Notes = s.Notes
                     }).ToList());


            result.AddRange(context.tasks.Where(c => c.IdfProject == idProject && c.IdfAssignedTo == null && c.IdfAssignableRol == null && c.State != "D" && c.IdfPeriod == idperiod && c.IdfAssignableRol == null)
                     .Select(s => new TasksForPlanningCustomEntity
                     {
                         Id = s.Id,
                         IdfTaskParent = s.IdfTaskParent,
                         IdfStatus = s.IdfStatus,
                         Address = s.Address,
                         Status = context.statuses.Where(c => c.Id == s.IdfStatus).FirstOrDefault().status,
                         AllDay = s.AllDay,
                         Description = s.Description,
                         Type = s.Type,
                         Deadline = s.Deadline,
                         State = s.State,
                         Lat = s.Lat,
                         Lon = s.Lon,
                         IdDuplicate = s.IdDuplicate,
                         IdfAssignedTo = s.IdfAssignedTo,
                         RecurrencePattern = s.RecurrencePattern,
                         RecurrenceException = s.RecurrenceException,
                         Subject = s.Subject,
                         IdfStaff = -1,
                         AssignedToPosition = string.Empty,
                         ProjectName = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().ProjectName,
                         ProjectColor = context.projects.Where(c => c.Id == s.IdfProject).FirstOrDefault().Color,
                         AssignedToColor = string.Empty,
                         AssignedToFullName = "Unassigned",
                         //AssignedToFullName = context.,
                         IdUser = -1,
                         Abm = string.Empty,
                         Hours = s.Hours * 3600,
                         IdfPeriod = s.IdfPeriod,
                         IdfAssignableRol = s.IdfAssignableRol,
                         Img = "",
                Notes = s.Notes
                     }).ToList());

         
            return result;
        }

        public List<tasks_reminders> GetTaskReminderByUser(long idUser)
        {
            return context.tasks_reminders
                    .Where(c => c.State == "C" &&
                           c.IdfTaskNavigation.IdfAssignedToNavigation.IdfStaffNavigation.IdfUser == idUser &&
                           // c.IdfTaskNavigation.Deadline.Value.ToShortDateString() == DateTime.Now.ToShortDateString() &&
                           c.IdfTaskNavigation.Deadline.Value.CompareTo(DateTime.Now) > 0  &&
                           // &&                                
                           // new DateTime(c.IdfTaskNavigation.Deadline.Value.Year, c.IdfTaskNavigation.Deadline.Value.Month, c.IdfTaskNavigation.Deadline.Value.Day, c.IdfTaskNavigation.Deadline.Value.Hour, c.IdfTaskNavigation.Deadline.Value.Minute, 0)
                           // .CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0).AddMinutes(c.IdfSettingReminderTimeNavigation.MinutesBefore)) == 0 &&                                 
                           c.IdfTaskNavigation.State == "C" &&
                           c.IdfTaskNavigation.IdfStatus != 4 &&
                           c.IdfTaskNavigation.IdfStatus != 5)
                    .Include(v => v.IdfTaskNavigation)
                             .ThenInclude(n => n.IdfProjectNavigation)
                    .Include(c => c.IdfTaskNavigation)
                         .ThenInclude(a => a.IdfAssignedToNavigation)
                         .ThenInclude(b => b.IdfStaffNavigation)
                         .ThenInclude(c => c.IdfUserNavigation)
                    .Include(d => d.IdfSettingReminderTimeNavigation)                    
                    .Distinct()
                    .Take(30)
                    .ToList();   
        }
    }
}