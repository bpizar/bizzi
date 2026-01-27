using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;
using Jaygor.People.Bussinness.helpers;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public IEnumerable<ProjectCustomEntity> GetProjects()
        {
            return dataAccessLayer.GetProjects().ToList();
        }

        public IEnumerable<ProjectCustomEntity> GetProjectsByUser(string username)
        {
            var res = dataAccessLayer.GetProjectsByUser(username).DistinctBy(c => c.Id);
            return res;
            // return dataAccessLayer.GetProjectsByUser(username);
        }

        public void GetProject(long id,
                               out ProjectCustomEntity project,
                               //out List<ProjectOwnersCustom> owners,
                               out List<settings_reminder_time> settingsReminderTime,
                               out List<ClientCustomEntity> clientsAllPeriods)
        {
            // out List<ClientCustomEntity> clients
            project = dataAccessLayer.GetProject(id);

            // clients = new List<ClientCustomEntity>();

            if (project == null)
            {
                project = new ProjectCustomEntity();
            }

            var tasks = dataAccessLayer.GetTasksByProject(id).ToList();
            project.TotalHours = tasks.Count() > 0 ? tasks.Sum(c => c.Hours) : 0;

            // owners = dataAccessLayer.GetProjectOwners(id).ToList();
            settingsReminderTime = dataAccessLayer.GetSettingsReminderTime();

            // clients = dataAccessLayer.GetClientsByProjects(id).ToList();
            // clients = dataAccessLayer.GetClientsByProjects(id, idperiod).ToList();
            clientsAllPeriods = dataAccessLayer.GetClientsByProjectsAllPeriods(id).DistinctBy(c => c.IdfClient).ToList();
        }

        public void GetProject2(long id,
                              long idperiod,
                              out List<TasksForPlanningCustomEntity> tasks,
                              out List<StaffCustomEntity> staffs,
                              out long taskHours,
                              out List<tasks_reminders> taskRemindes,
                               out List<ClientCustomEntity> clients
                               )
        {
            tasks = dataAccessLayer.GetTasksByProject2(id, idperiod).ToList();

            // fake
            // staffPeriodSettings = dataAccessLayer.GetStaffPeriodSettings(id,idperiod)
            tasks.ForEach(c => c.Hours = c.Hours / 3600);


            taskHours = tasks.Sum(c => c.Hours);
            clients = dataAccessLayer.GetClientsByProjects(id, idperiod).ToList();
            staffs = dataAccessLayer.GetStaffsByProject2(id, idperiod).ToList();
            taskRemindes = dataAccessLayer.GetTaskRemindes(idperiod);
        }

        // no use OUT and not REF
        public long SaveProject(ProjectCustomEntity project,
                                 List<TasksForPlanningCustomEntity> tasks,
                                 List<Staff_Project_PositionCustomEntity> staffs,
                                 List<ProjectOwnersCustom> owners,
                                 List<tasks_reminders> tasksReminders,
                                string username,
                                long idperiod,
                                bool isadmin,
                                List<ClientCustomEntity> clients,
                                out List<tasks_reminders> taskReminders,
                                out List<TasksForPlanningCustomEntity> tasksOut,
                                out List<StaffCustomEntity> staffsOut,
                                out long taskHoursOut,
                                out List<tasks_reminders> taskRemindersOut,
                                out List<ClientCustomEntity> clientsOut,
                                bool ForceGetData,
                                out bool returningData)
        {
            var output = dataAccessLayer.SaveProject(project, tasks, staffs, owners, tasksReminders, username, idperiod, isadmin, clients);

            taskReminders = new List<tasks_reminders>();
            tasksOut = new List<TasksForPlanningCustomEntity>();
            staffsOut = new List<StaffCustomEntity>();
            taskHoursOut = 0;
            taskRemindersOut = new List<tasks_reminders>();
            clientsOut = new List<ClientCustomEntity>();

            returningData = staffs.Any() || ForceGetData || tasksReminders.Any();

            if (staffs.Any() || clients.Any() || ForceGetData || tasksReminders.Any())
            {
                var projectIdForCall = project.Id <= 0 ? output : project.Id;
                tasksOut = dataAccessLayer.GetTasksByProject2(projectIdForCall, idperiod).ToList();
                tasksOut.ForEach(c => c.Hours = c.Hours / 3600);
                taskHoursOut = tasks.Sum(c => c.Hours);
                clientsOut = dataAccessLayer.GetClientsByProjects(projectIdForCall, idperiod).ToList();
                staffsOut = dataAccessLayer.GetStaffsByProject2(projectIdForCall, idperiod).ToList();
                taskRemindersOut = dataAccessLayer.GetTaskRemindes(idperiod);
                taskReminders = dataAccessLayer.GetTaskRemindes(idperiod);
            }

            return output;

            // return dataAccessLayer.SaveProject(project, tasks, staffs, owners, tasksReminders,username, idperiod, isadmin, clients);                   
        }

        public bool DeleteProject(ProjectCustomEntity project, long idperiod)
        {
            return dataAccessLayer.DeleteProject(project, idperiod);
        }


        public List<ProjectPettyCashCustom> GetPettyCash(long id, long idproject, long idcategory)
        {
            return idcategory == -1 ?
                                dataAccessLayer.GetPettyCash(id, idproject).ToList().ToList() :
                               dataAccessLayer.GetPettyCashByCategory(id, idproject, idcategory).ToList().ToList();
        }

        public List<project_pettycash_categories> GetPettyCashCategories()
        {
            return dataAccessLayer.GetPettyCashCategories();
        }

        public CommonResponse SavePettyCash(List<ProjectPettyCashCustom> pettyCash, string username, long idperiod)
        {
            return dataAccessLayer.SavePettyCash(pettyCash, username, idperiod);
        }

        public void GetTasksByProject(long idproject,
                                      long idperiod,
                                      out List<TasksForPlanningCustomEntity> tasks)
        {
            tasks = dataAccessLayer.GetTasksByProject2(idproject, idperiod).ToList();
        }

        public void GetOverdueTasks(out List<TasksForPlanningCustomEntity> tasks)
        {
            tasks = dataAccessLayer.GetOverdueTasks().ToList();
        }

        public void GetStaffAndPositionsForCopyWindow(long id,
                            long idperiod,
                            out List<StaffCustomEntity> staffs,
                            out List<positions> positions)
        {
            staffs = dataAccessLayer.GetStaffForCopyWindow(id, idperiod).ToList();
            positions = dataAccessLayer.GetPositionsForCopyWindow(id, idperiod).ToList();
        }







    }
}