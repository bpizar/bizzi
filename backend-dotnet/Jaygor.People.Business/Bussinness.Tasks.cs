using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.Responses;
using Microsoft.Extensions.Configuration;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {

        //public List<GenericPair> GetTasksForDashboard1(out string periodDescription)
        //{
        //    //periodDescription;
        //    return dataAccessLayer.GetTasksForDashboard1(out periodDescription); 
        //}

        public bool SaveTask(tasks task, duplicate_tasks duplicateTask, Boolean editingSerie)
        {
            if (duplicateTask!=null && duplicateTask.EndOn != null)
            {
                duplicateTask.EndOn = new DateTime(duplicateTask.EndOn.Value.Year, duplicateTask.EndOn.Value.Month, duplicateTask.EndOn.Value.Day,23,59,59,0); 
            }

            return dataAccessLayer.SaveTask(task, duplicateTask, editingSerie);
        }

        public bool MoveCopyTask(tasks task, bool move, long idfProject, long idfPeriod)
        {
            //return dataAccessLayer.MoveCopyTask(task, IdfProject, IdfPeriod, move);
            return dataAccessLayer.MoveCopyTask(task, move,  idfProject,  idfPeriod);
        }
        public tasks GetTask(long id)
        {
            return dataAccessLayer.GetTask(id);
        }

        public void GetTasks(DateTime from, 
            DateTime to,
            out List<TasksForPlanningCustomEntity> TasksForPlanningCustomEntity,
            out List<duplicate_tasks> Duplicates,
            out List<TasksForPlanningCustomEntity> UnassignedTasks)
        {
            TasksForPlanningCustomEntity = new List<TasksForPlanningCustomEntity>();
            UnassignedTasks = new List<TasksForPlanningCustomEntity>();
            Duplicates = new List<duplicate_tasks>();
            TasksForPlanningCustomEntity.AddRange(dataAccessLayer.GetTasks(from, to));
            UnassignedTasks.AddRange(dataAccessLayer.GetUnasignedTasks(from, to));
        }

        public void GetTasksByStaff(long id, 
                                 long idperiod, 
                                    int workingHoursByPeriodStaff,
                                 out List<TasksForPlanningCustomEntity> tasks,
                                 out long AssignedScheduledHours,
                                 out long AssignedTasksHours,
                                 out staff_period_settings staffPeriodSettings,
                                 out string AssignedProgramsToAux)
        {
            AssignedProgramsToAux = string.Empty;


            var projectsAux = dataAccessLayer.GetProjectsByStaff(id, idperiod);

            var s = string.Empty;
            projectsAux.ToList().ForEach(c => s = string.Format("{0}"  +  (string.IsNullOrEmpty(s) ? "" : " , ") + "{1}", s , c.ProjectName) );
            AssignedProgramsToAux = s;

            staffPeriodSettings = dataAccessLayer.GetStaffPeriodSettings(id, idperiod); //  new List<staff_period_settings>();

            if (staffPeriodSettings == null)
            {
                dataAccessLayer.SaveStaffPeriodSettings(id, idperiod, workingHoursByPeriodStaff);
                staffPeriodSettings = dataAccessLayer.GetStaffPeriodSettings(id, idperiod); //  new List<staff_period_settings>();
			}


            var scheduled = dataAccessLayer.GetSchedulingByStaff(id, idperiod).ToList();
            tasks = dataAccessLayer.GetTasksByStaff(id, idperiod).ToList();
            AssignedScheduledHours = scheduled.Sum(c => c.Hours) ; //Convert.ToDecimal(scheduled.Sum(c=>c.Hours));
            AssignedTasksHours = tasks.Sum(c => c.Hours); // Convert.ToDecimal(tasks.Sum(c=>c.Hours));
        }
        		
        public bool GetRemindersForPanel(out List<ReminderForPanel> ReminderToday,
                                         out List<ReminderForPanel> RemindersTomorrow,
                                         out List<ReminderForPanel> RemindersOthers,
                                         out List<ReminderForPanel> RemindersMedicals,
                                         string username)
        {
            ReminderToday = new List<ReminderForPanel>();
            RemindersTomorrow = new List<ReminderForPanel>();
            RemindersOthers = new List<ReminderForPanel>();
            RemindersMedicals = new List<ReminderForPanel>();

            var iduser = dataAccessLayer.IdentityGetUserByEmail(username).Id;
            var reminders = dataAccessLayer.GetTaskReminderByUser(iduser);

            ReminderToday.AddRange(reminders.Where(x => string.Compare(x.IdfTaskNavigation.Deadline.Value.ToShortDateString(), DateTime.Now.ToShortDateString(), StringComparison.Ordinal) == 0).Select(p=>new ReminderForPanel
            {
                DeadLine = p.IdfTaskNavigation.Deadline.Value.ToShortTimeString(),
                Description = p.IdfTaskNavigation.Description,
                Name = p.IdfTaskNavigation.Subject,
                IdTask = p.IdfTaskNavigation.Id,
                ProjectName = p.IdfTaskNavigation.IdfProjectNavigation.ProjectName,
                Color = p.IdfTaskNavigation.IdfProjectNavigation.Color
            }).DistinctBy(c=>c.IdTask).ToList());

            var tomorrow = DateTime.Now.AddDays(1).ToShortDateString();

            RemindersTomorrow.AddRange(reminders.Where(x =>  x.IdfTaskNavigation.Deadline.Value.ToShortDateString() == tomorrow).Select(p => new ReminderForPanel
            {
                DeadLine = p.IdfTaskNavigation.Deadline.Value.ToShortTimeString(),
                Description = p.IdfTaskNavigation.Description,
                Name = p.IdfTaskNavigation.Subject,
                IdTask = p.IdfTaskNavigation.Id,
                ProjectName = p.IdfTaskNavigation.IdfProjectNavigation.ProjectName,
                Color = p.IdfTaskNavigation.IdfProjectNavigation.Color
            }).DistinctBy(c => c.IdTask).ToList());

            //var dateLater = 

            RemindersOthers.AddRange(reminders.Where(x => x.IdfTaskNavigation.Deadline.Value.CompareTo(DateTime.Now.AddDays(1)) >= 0).Select(p => new ReminderForPanel
            {
                DeadLine = p.IdfTaskNavigation.Deadline.Value.ToShortDateString(),
                Description = p.IdfTaskNavigation.Description,
                Name = p.IdfTaskNavigation.Subject,
                IdTask = p.IdfTaskNavigation.Id,
                ProjectName = p.IdfTaskNavigation.IdfProjectNavigation.ProjectName,
                Color = p.IdfTaskNavigation.IdfProjectNavigation.Color
            }).DistinctBy(c => c.IdTask));

            //  List<h_medical_remindersCustom> GetMedicalRemindersByUser(long idUser)
            RemindersMedicals.AddRange(dataAccessLayer.GetMedicalRemindersByUser(iduser).Select(x => new ReminderForPanel
            {
                DeadLine = x.Datetime.ToShortTimeString(),
                Description =  x.Client ,
                Name = x.Description,  
                IdTask = x.Id,
                ProjectName = x.ProjectName,
                Color= x.Color
            }).DistinctBy(c=>c.IdTask)) ;


            return true;
        }

    }
}
