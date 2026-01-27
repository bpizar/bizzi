//using JayGor.People.DataAccess.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Responses;
using JayGor.People.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using JayGor.People.DataAccess.MySql;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public IEnumerable<ProjectOwnersCustom> GetProjectOwners(long idProject, long periodId)
        {
            return context.project_owners
                                    .Where(c => c.IdfProject == idProject && c.State != "D" && c.IdfPeriod == periodId)
                                    .Select(s => new ProjectOwnersCustom
                                    {
                                        Id = s.Id,
                                        IdfStaff = s.IdfOwnerNavigation.Id,
                                        IdfOwner = s.IdfOwner,
                                        IdfProject = s.IdfProject,
                                        State = s.State,
                                        FullName = string.Format("{0} {1}", s.IdfOwnerNavigation.IdfUserNavigation.LastName, s.IdfOwnerNavigation.IdfUserNavigation.FirstName)
                                    }).ToList();
        }

        public IEnumerable<ProjectCustomEntity> GetProjects()
        {
            return context.projects
                        .Where(c => c.State != "D")
                        .Select(s => new ProjectCustomEntity
                        {
                            Id = s.Id,
                            BeginDate = s.BeginDate,
                            Color = s.Color,
                            CreationDate = s.CreationDate,
                            Description = s.Description,
                            EndDate = s.EndDate,
                            ProjectName = s.ProjectName,
                            Address = s.Address,
                            Phone1 = s.Phone1,
                            Phone2 = s.Phone2,
                            State = s.State,
                            TotalHours = context.tasks.Where(c => c.State != "D" && c.IdfProject == s.Id).FirstOrDefault() != null ? context.tasks.Where(c => c.State != "D" && c.IdfProject == s.Id).Sum(c => c.Hours) : 0,
                            Visible = s.Visible
                        }).ToList();               
        }

        public IEnumerable<ProjectCustomEntity> GetProjectsByUser(string username)
        {
          
            var idstaff = context.staff.Where(c => c.IdfUserNavigation.Email == username).FirstOrDefault().Id;
            return context.projects
                            .Join(context.project_owners,
                               pro => pro.Id,
                               po => po.IdfProject,
                               (pro, po) => new { pro, po })
                               .Where(cc => cc.po.State != "D" && cc.po.IdfOwner == idstaff )
                               .Distinct()
                               .Select(s => new ProjectCustomEntity
                               {
                                   Id = s.pro.Id,
                                   BeginDate = s.pro.BeginDate,
                                   Color = s.pro.Color,
                                   CreationDate = s.pro.CreationDate,
                                   Description = s.pro.Description,
                                   EndDate = s.pro.EndDate,
                                   ProjectName = s.pro.ProjectName,
                                   Address = s.pro.Address,
                                   Phone1 = s.pro.Phone1,
                                   Phone2 = s.pro.Phone2,
                                   State = s.pro.State,
                                   TotalHours = context.tasks.Where(c => c.State != "D" && c.IdfProject == s.pro.Id).FirstOrDefault() != null ? context.tasks.Where(c => c.State != "D" && c.IdfProject == s.pro.Id).Sum(c => c.Hours) : 0,
                                   Visible = s.pro.Visible
                               }).ToList();
               
        }

        public List<settings_reminder_time> GetSettingsReminderTime()
        {
            return context.settings_reminder_time
                                     .Where(c => c.State != "D")
                                     .Select(s => new settings_reminder_time
                                     {
                                         Id = s.Id,
                                         MinutesBefore = s.MinutesBefore
                                     }).ToList();
        }

        public ProjectCustomEntity GetProject(long id)
        {
            return context.projects.Where(c => c.Id == id)
                                     .Select(s => new ProjectCustomEntity
                                     {
                                         Id = s.Id,
                                         BeginDate = s.BeginDate,
                                         Color = s.Color,
                                         CreationDate = s.CreationDate,
                                         Description = s.Description,
                                         EndDate = s.EndDate,
                                         ProjectName = s.ProjectName,
                                         Address = s.Address,
                                         Phone1 = s.Phone1,
                                         Phone2 = s.Phone2,
                                         TotalHours = 34, // ????
                                        State = s.State,
                                         Visible = s.Visible,
                                         City = s.City
                                     }).FirstOrDefault();
        }

        private void savetasksReminders(MySqlContextDB contextAux, List<tasks_reminders> tasksReminders)
        {
            foreach (var tr in tasksReminders)
            {
                var item = contextAux.tasks_reminders.Where(c => c.Id == tr.Id || (c.IdfTask == tr.IdfTask && c.IdfSettingReminderTime == tr.IdfSettingReminderTime && c.IdfPeriod == tr.IdfPeriod)).SingleOrDefault();

                if (item != null)
                {
                    item.State = tr.State == "true" ? "C" : "D";
                }
                else
                {
                    var newItem = new tasks_reminders
                    {
                        IdfPeriod = tr.IdfPeriod,
                        IdfSettingReminderTime = tr.IdfSettingReminderTime,
                        IdfTask = tr.IdfTask,
                        State = tr.State == "true" ? "C" : "D"
                    };

                    contextAux.tasks_reminders.Add(newItem);
                }
            }
        }

        public long SaveProject(ProjectCustomEntity project,
                   List<TasksForPlanningCustomEntity> tasks,
                   List<Staff_Project_PositionCustomEntity> staffs,
                   List<ProjectOwnersCustom> owners,
                   List<tasks_reminders> tasksReminders,
                   string username,
                   long idperiod,
                   bool isadmin,
                   List<ClientCustomEntity> clients)
        {
            long newStaffIdAux = -1;
            long result = 0;
            Exception error = null;

            var transaction = context.Database.BeginTransaction();

        
            try
            {                     
                if (!isadmin)
                {
                    if (project.abm == "I" || project.abm == "D")
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        var isowner = context.project_owners.Where(c => c.IdfProject == project.Id && c.State != "D").Any() && 
                                      context.project_owners.Where(c => c.IdfProject == project.Id && c.State != "D").
                                      Where(x => x.IdfOwnerNavigation.IdfUserNavigation.Email == username).Any();

                           if (!isowner)
                        {
                            throw new UnauthorizedAccessException();
                        }
                    }
                }

                switch (project.abm)
                {
                    case "I":
                        var newProject = new projects
                        {
                            Description = project.Description,
                            ProjectName = project.ProjectName,
                            Address = project.Address,
                            Phone1 = project.Phone1,
                            Phone2 = project.Phone2,
                            State = "C",
                            Color = project.Color,
                            BeginDate = project.BeginDate,
                            EndDate = project.EndDate,
                            Visible = 1,
                            City = project.City
                        };

                        context.projects.Add(newProject);
                        context.SaveChanges();
                        result = newProject.Id;

                        if (staffs != null)
                        {
                            foreach (var s in staffs.Where(c => c.Abm == "I"))
                            {
                                newStaffIdAux = s.Id;

                                context.staff_project_position
                                       .Where(x => x.IdfPosition == s.IdfPosition && x.IdfProject == newProject.Id && x.IdfStaff == s.IdfStaff && x.IdfPeriod == idperiod && x.State == "C")
                                       .ToList()
                                       .ForEach(z => z.State = "D");

                                context.SaveChanges();

                                var newStaff = new staff_project_position
                                {
                                    Hours = s.Hours,
                                    IdfPosition = s.IdfPosition,
                                    IdfProject = newProject.Id,
                                    IdfStaff = s.IdfStaff,
                                    IdfPeriod = idperiod,
                                    State = "C",
                                };

                                context.staff_project_position.Add(newStaff);

                                tasks.Where(c => c.IdfAssignedTo == newStaffIdAux).ToList().ForEach(c => c.IdfAssignedTo = newStaff.Id);
                                context.SaveChanges();
                            }
                        }

                        if (tasks != null)
                        {
                            foreach (var t in tasks.Where(c => c.Abm == "I"))
                            {
                                var idTaskAux = t.Id;
                                List<TasksForPlanningCustomEntity> listSubtask = new List<TasksForPlanningCustomEntity>();//
                                listSubtask = tasks.FindAll(p => p.IdfTaskParent == t.Id).ToList();//
                                var newTask = new tasks
                                {
                                    IdfTaskParent = t.IdfTaskParent,
                                    CreationDate = DateTime.Now,
                                    IdfStatus = t.IdfStatus,
                                    Description = t.Description,
                                    IdfAssignedTo = t.IdfAssignedTo <= 0 ? null : t.IdfAssignedTo,
                                    IdfAssignableRol = t.IdfAssignableRol <= 0 ? null : t.IdfAssignableRol,
                                    Deadline = t.Deadline,
                                    IdfPeriod = t.IdfPeriod,
                                    IdfProject = newProject.Id,
                                    Subject = t.Subject,
                                    State = "C",
                                    Hours = t.Hours,
                                    Type = t.Type,
                                    Notes = t.Notes
                                };

                                context.tasks.Add(newTask);
                                context.SaveChanges();

                                tasksReminders.Where(c => c.IdfTask == idTaskAux).ToList().ForEach(c => c.IdfTask = newTask.Id);
                                listSubtask.ForEach(p =>
                                {
                                    var newSubtask = new tasks
                                    {
                                        IdfTaskParent = newTask.Id,
                                        CreationDate = DateTime.Now,
                                        IdfStatus = p.IdfStatus,
                                        Description = p.Description,
                                        IdfAssignedTo = p.IdfAssignedTo <= 0 ? null : p.IdfAssignedTo,
                                        IdfAssignableRol = p.IdfAssignableRol <= 0 ? null : p.IdfAssignableRol,
                                        Deadline = p.Deadline,
                                        IdfPeriod = p.IdfPeriod,
                                        IdfProject = newProject.Id,
                                        Subject = p.Subject,
                                        State = "C",
                                        Hours = p.Hours,
                                        Type = p.Type
                                    };
                                    context.tasks.Add(newSubtask);
                                    context.SaveChanges();
                                });
                            }
                        }

                        if (owners != null)
                        {
                            foreach (var o in owners)
                            {
                                var newOwner = new project_owners
                                {
                                    IdfOwner = o.IdfOwner,
                                    IdfProject = newProject.Id,
                                    IdfPeriod = idperiod,
                                    State = "C"
                                };

                                context.project_owners.Add(newOwner);
                            }
                        }

                        context.SaveChanges();

                        this.savetasksReminders(context, tasksReminders);

                        if (clients != null)
                        {
                            foreach (var c in clients.Where(c => c.Abm == "I"))
                            {

                                var projectclientAux = context.projects_clients.Where(x => x.IdfClient == c.IdfClient && x.IdfPeriod == idperiod && x.IdfProject == newProject.Id && x.State == "C");


                                if(projectclientAux == null || projectclientAux.Count()<=0)
                                {
                                    var newProjectClient = new projects_clients
                                    {
                                        IdfClient = c.IdfClient,
                                        IdfPeriod = idperiod,
                                        IdfProject = newProject.Id,
                                        State = "C"
                                    };

                                    context.projects_clients.Add(newProjectClient);
                                }
                            }
                        }

                        context.SaveChanges();

                        break;

                    case "U":
                        var editProj = context.projects.Where(c => c.Id == project.Id).FirstOrDefault();
                        editProj.Description = project.Description;
                        editProj.ProjectName = project.ProjectName;

                        editProj.Address = project.Address;
                        editProj.City = project.City;

                        editProj.Phone1 = project.Phone1;
                        editProj.Phone2 = project.Phone2;

                        editProj.Color = project.Color;
                        editProj.BeginDate = project.BeginDate;
                        editProj.EndDate = project.EndDate;
                        editProj.State = project.State;
                        result = project.Id;

                        if (staffs != null)
                        {
                            foreach (var s in staffs)
                            {
                                switch (s.Abm)
                                {
                                    case "I":
                                        newStaffIdAux = s.Id;

                                        context.staff_project_position
                                           .Where(x => x.IdfPosition == s.IdfPosition && x.IdfProject == s.IdfProject && x.IdfStaff == s.IdfStaff && x.IdfPeriod == idperiod && x.State == "C")
                                           .ToList()
                                           .ForEach(z => z.State = "D");

                                        context.SaveChanges();

                                        var newStaff = new staff_project_position
                                        {
                                            Hours = s.Hours,
                                            IdfPosition = s.IdfPosition,
                                            IdfProject = editProj.Id,
                                            IdfStaff = s.IdfStaff,
                                            IdfPeriod = idperiod,
                                            State = "C",
                                        };

                                        context.staff_project_position.Add(newStaff);
                                        context.SaveChanges();                                                
                                        tasks.Where(c => c.IdfAssignedTo == newStaffIdAux).ToList().ForEach(c => c.IdfAssignedTo = newStaff.Id);
                                        break;
                                    case "D":

                                        if (s.Id > 0)
                                        {                                                    
                                            var staffProjectPosition = context.staff_project_position.Where(c => c.Id == s.Id);
                                            if (staffProjectPosition != null)
                                            {
                                                staffProjectPosition.ToList().ForEach(f => f.State = "D");
                                            }

                                            context.tasks.Where(c => c.State != "D" && c.IdfAssignedTo != null && c.IdfAssignedTo == s.Id && c.IdfPeriod == idperiod).ToList().ForEach(c => c.IdfAssignedTo = null);
                                            context.scheduling.Where(c => c.IdfAssignedTo == s.Id && c.IdfPeriod == idperiod).ToList().ForEach(c => c.State = "D");
                                            context.SaveChanges();
                                        }

                                        break;
                                }
                            }
                        }
                        if (tasks != null)
                        {
                            foreach (var t in tasks)
                            {
                                switch (t.Abm)
                                {
                                    case "I":

                                        var idTaskAux = t.Id;

                                        var newTask = new tasks
                                        {
                                            IdfTaskParent = t.IdfTaskParent,
                                            CreationDate = DateTime.Now,
                                            IdfStatus = t.IdfStatus,
                                            Description = t.Description,
                                            Deadline = t.Deadline,

                                            IdfAssignedTo = t.IdfAssignedTo <= 0 ? null : t.IdfAssignedTo,
                                            IdfAssignableRol = t.IdfAssignableRol <= 0 ? null : t.IdfAssignableRol,


                                            IdfProject = editProj.Id,
                                            Subject = t.Subject,
                                            State = "C",
                                            IdfPeriod = t.IdfPeriod,
                                            Hours = t.Hours,
                                            Type = t.Type,
                                            Notes = t.Notes
                                        };

                                        context.tasks.Add(newTask);
                                        context.SaveChanges();
                                        tasksReminders.Where(c => c.IdfTask == idTaskAux).ToList().ForEach(d => d.IdfTask = newTask.Id);

                                        break;

                                    case "U":
                                        var editTask = context.tasks.Where(c => c.Id == t.Id).FirstOrDefault();

                                        editTask.Subject = t.Subject;
                                        editTask.Description = t.Description;
                                        editTask.IdfStatus = t.IdfStatus;

                                        editTask.Hours = t.Hours;

                                        editTask.IdfAssignedTo = t.IdfAssignedTo <= 0 ? null : t.IdfAssignedTo;
                                        editTask.IdfAssignableRol = t.IdfAssignableRol <= 0 ? null : t.IdfAssignableRol;

                                        editTask.Deadline = t.Deadline;

                                        editTask.Notes = t.Notes;

                                        break;

                                    case "D":
                                        var deleteTask = context.tasks.Where(c => c.Id == t.Id).FirstOrDefault();
                                        deleteTask.State = "D";
                                        break;
                                }
                            }
                        }

                        foreach (var todelete in context.project_owners.Where(c => c.State != "D" && c.IdfProject == project.Id))
                        {
                            var filter = owners.Where(c => c.IdfOwner == todelete.IdfOwner);

                            if (filter == null || filter.Count() == 0)
                            {
                                todelete.State = "D";
                            }
                        }

                        context.SaveChanges();

                        if (owners != null)
                        {
                            foreach (var o in owners)
                            {
                                var search = context.project_owners.Where(c => c.IdfPeriod == idperiod && c.State != "D" && c.IdfProject == project.Id && c.IdfOwner == o.IdfOwner);

                                if (search == null || search.Count() <= 0)
                                {
                                    var newOwner = new project_owners
                                    {
                                        IdfOwner = o.IdfOwner,
                                        IdfProject = project.Id,
                                        IdfPeriod = idperiod,
                                        State = "C"
                                    };

                                    context.project_owners.Add(newOwner);
                                }
                            }
                        }

                        context.SaveChanges();
                        this.savetasksReminders(context, tasksReminders);

                        if (clients != null)
                        {
                            foreach (var c in clients.Where(c => c.Abm == "I"))
                            {
                                var projectclientAux = context.projects_clients.Where(x => x.IdfClient == c.IdfClient && x.IdfPeriod == idperiod && x.IdfProject == project.Id && x.State == "C");


                                if (projectclientAux == null || projectclientAux.Count() <= 0)
                                {
                                    var newProjectClient = new projects_clients
                                    {
                                        IdfClient = c.IdfClient,
                                        IdfPeriod = idperiod,
                                        IdfProject = editProj.Id,
                                        State = "C"
                                    };

                                    context.projects_clients.Add(newProjectClient);
                                }
                            }

                            foreach (var c in clients.Where(c => c.Abm == "D"))
                            {
                                var forDelete = context.projects_clients.Where(d => d.Id == c.Id).FirstOrDefault(); // ().State = c.Abm;
                                if (forDelete != null)
                                {
                                    forDelete.State = c.Abm;
                                }

                            }
                        }

                        context.SaveChanges();

                        break;

                    case "D":

                        context.projects.Where(c => c.Id == project.Id).FirstOrDefault().State = "D";
                        context.staff_project_position.Where(c => c.IdfProject == project.Id).ToList().ForEach(c => c.State = "D");
                        context.tasks.Where(c => c.IdfProject == project.Id && c.IdfPeriod == idperiod).ToList().ForEach(c => c.State = "D");
                        context.scheduling.Where(c => c.IdfProject == project.Id && c.IdfPeriod == idperiod).ToList().ForEach(c => c.State = "D");
                        result = project.Id;

                        break;
                    default:
                        break;
                }

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                error = ex;
            }
                                  
            return result;
        }


        public IEnumerable<ProjectPettyCashCustom> GetPettyCash(long id, long idperiod)
        {
            return context.project_petty_cash.Where(c => c.State != "D" && c.IdfProject == id && c.IdfPeriod == idperiod)
                        .Select(p => new ProjectPettyCashCustom
                        {
                            Id = p.Id,
                            Amount = p.Amount,
                            Date = p.Date,
                            Description = p.Description,
                            IdfProject = p.IdfProject,
                            Abm = string.Empty,
                            State = p.State,
                            Category = p.IdfCategoriesNavigation.Description,
                            IdfCategories = p.IdfCategories
                        }).ToList();                
        }

        public IEnumerable<ProjectPettyCashCustom> GetPettyCashByCategory(long idProject, long idperiod, long idcategory)
        {
           return context.project_petty_cash.Where(c => c.State != "D" && c.IdfProject == idProject && c.IdfPeriod == idperiod && c.IdfCategories == idcategory)
                        .Select(p => new ProjectPettyCashCustom
                        {
                            Id = p.Id,
                            Amount = p.Amount,
                            Date = p.Date,
                            Description = p.Description,
                            IdfProject = p.IdfProject,
                            Abm = string.Empty,
                            State = p.State,
                            Category = p.IdfCategoriesNavigation.Description,
                            IdfCategories = p.IdfCategories
                        }).ToList();                
        }

        public CommonResponse SavePettyCash(List<ProjectPettyCashCustom> pettyCash,
                                                                  string username,
                                                                  long idperiod)
        {
            var transaction = context.Database.BeginTransaction();
            var response = new CommonResponse();

            try
            {
                foreach (var pc in pettyCash)
                {
                    switch (pc.Abm)
                    {
                        case "D":
                            if (pc.Id > 0)
                            {
                                context.project_petty_cash.Where(c => c.Id == pc.Id).Single().State = pc.Abm;
                            }

                            break;

                        default:
                            var newPetty = new project_petty_cash
                            {
                                Amount = Convert.ToDecimal(pc.Amount), // OJO PUEDE GENERAR ERROR
                                Date = pc.Date,
                                Description = pc.Description,
                                IdfProject = pc.IdfProject,
                                RegistrationDate = DateTime.Now,
                                State = "C",
                                IdfPeriod = idperiod,
                                IdfCategories = pc.IdfCategories
                            };

                            context.project_petty_cash.Add(newPetty);

                            break;
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

            return response;
        }

        public List<project_pettycash_categories> GetPettyCashCategories()
        {
            return context.project_pettycash_categories.Where(c => c.State != "D").ToList();
        }

        public IEnumerable<ProjectCustomEntity> GetProjectsByStaff(long idStaff, long idperiod)
        {
            return context.staff_project_position
                            .Where(c => c.IdfStaff == idStaff && c.State != "D" && c.IdfPeriod == idperiod)
                                   .Select(p => new ProjectCustomEntity
                                   {
                                       Id = p.Id,
                                       ProjectName = string.Format("{0} POSITION: {1}", p.IdfProjectNavigation.ProjectName ,  p.IdfPositionNavigation.Name),
                                       State = p.State,
                                   }).ToList();
        }

        public IEnumerable<ProjectCustomEntity> GetProjectsByClient(long idclient, long idperiod)
        {
            try
            {
                return context.projects_clients
                                         .Where(c => c.IdfClient == idclient && c.State != "D" && c.IdfPeriod == idperiod)
                                                .Select(p => new ProjectCustomEntity
                                                {
                                                    Id = p.Id,
                                                    ProjectName = string.Format("{0} {1} ", p.IdfProjectNavigation.ProjectName,
                                                                                             p.IdfProjectNavigation.project_owners.FirstOrDefault() != null ?
                                                                                                 string.Format("WORKER : {0} {1}", context.identity_users.Where(c => c.State != "D" && c.Id == p.IdfProjectNavigation.project_owners.FirstOrDefault(x => x.State != "D").IdfOwnerNavigation.IdfUser).Single().LastName, context.identity_users.Where(c => c.Id == p.IdfProjectNavigation.project_owners.FirstOrDefault(x => x.State != "D").IdfOwnerNavigation.IdfUser).Single().FirstName)
                                                                                                 : ""),
                                                    State = p.State,
                                                }).ToList();
            }catch(Exception ex)
            {
                return null;
            }
        }

        public bool DeleteProject(ProjectCustomEntity project, long idperiod)                  
        {
            var transaction = context.Database.BeginTransaction();
            bool result = true;

            try
            {                    
                context.projects.Where(c => c.Id == project.Id).FirstOrDefault().State = "D";
                context.staff_project_position.Where(c => c.IdfProject == project.Id).ToList().ForEach(c => c.State = "D");
                context.tasks.Where(c => c.IdfProject == project.Id && c.IdfPeriod == idperiod).ToList().ForEach(c => c.State = "D");
                context.scheduling.Where(c => c.IdfProject == project.Id && c.IdfPeriod == idperiod).ToList().ForEach(c => c.State = "D");
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                result = false;
            }

            return result;
        }

    }
} 