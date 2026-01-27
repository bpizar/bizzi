using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Enumerators;
using JayGor.People.Entities.Responses;
using Microsoft.EntityFrameworkCore;


namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService    
    {
        public IEnumerable<ProjectFormReminderUsersCustomEntity> GetAllProjectFormReminderUsers()
        {
            return context.project_form_reminder_users
                                    .Select(p => new ProjectFormReminderUsersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectFormReminder = p.IdfProjectFormReminder,
                                        IdfUser = p.IdfUser,
                                    }).ToList();
        }

        public ProjectFormReminderUsersCustomEntity GetProjectFormReminderUserbyId(long id)
        {
            return context.project_form_reminder_users
                        .Where(c => c.Id == id)
                                    .Select(p => new ProjectFormReminderUsersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectFormReminder = p.IdfProjectFormReminder,
                                        IdfUser = p.IdfUser
                                    }).FirstOrDefault();
        }

        public CommonResponse SaveProjectFormReminderUser(project_form_reminder_users ProjectFormReminderUser)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                project_form_reminder_users projectFormReminderUserFromDB = null;
                if (ProjectFormReminderUser.Id > 0)
                {
                    projectFormReminderUserFromDB = context.project_form_reminder_users.Where(p => p.Id == ProjectFormReminderUser.Id).SingleOrDefault();
                }
                if (projectFormReminderUserFromDB == null)
                {
                    ProjectFormReminderUser.Id = 0;
                    context.project_form_reminder_users.Add(ProjectFormReminderUser);
                    context.SaveChanges();
                    projectFormReminderUserFromDB = context.project_form_reminder_users.Where(p => p.Id == ProjectFormReminderUser.Id).Single();
                }
                else
                {
                    projectFormReminderUserFromDB.IdfProjectFormReminder = ProjectFormReminderUser.IdfProjectFormReminder;
                    projectFormReminderUserFromDB.IdfUser = ProjectFormReminderUser.IdfUser;
                    context.project_form_reminder_users.Update(projectFormReminderUserFromDB);
                    context.SaveChanges();
                }
                transaction.Commit();
                result.Result = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return result;
        }

        public CommonResponse DeleteProjectFormReminderUsers(long projectFormReminderUserId)
        {
            CommonResponse result = new CommonResponse(); 
            var ProjectFormReminderUserToDelete = GetProjectFormReminderUserbyId(projectFormReminderUserId);
            if (ProjectFormReminderUserToDelete == null)
                result.Result = false;
            context.project_form_reminder_users.Remove(ProjectFormReminderUserToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<ProjectFormReminderUsersCustomEntity> GetAllProjectFormReminderUsersByProjectFormReminder(long idProjectFormReminder)
        {
            return context.project_form_reminder_users.Where(p=>p.IdfProjectFormReminder== idProjectFormReminder)
                                    .Select(p => new ProjectFormReminderUsersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectFormReminder = p.IdfProjectFormReminder,
                                        IdfUser = p.IdfUser,
                                    }).ToList();
        }

    }
}
