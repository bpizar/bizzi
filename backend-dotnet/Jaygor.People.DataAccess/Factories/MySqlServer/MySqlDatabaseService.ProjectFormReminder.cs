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
        public IEnumerable<ProjectFormRemindersCustomEntity> GetAllProjectFormReminders()
        {
            return context.project_form_reminders
                                    .Select(p => new ProjectFormRemindersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfReminderLevel = p.IdfReminderLevel,
                                        IdfPeriodType = p.IdfPeriodType,
                                        IdfPeriodValue = p.IdfPeriodValue,
                                        IdfUsers = context.project_form_reminder_users.Where(q => q.IdfProjectFormReminder == p.Id).Select(r => r.IdfUser).ToArray()
                                    }).ToList();
        }

        public ProjectFormRemindersCustomEntity GetProjectFormReminderbyId(long id)
        {
            return context.project_form_reminders
                        .Where(c => c.Id == id)
                                    .Select(p => new ProjectFormRemindersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfReminderLevel = p.IdfReminderLevel,
                                        IdfPeriodType = p.IdfPeriodType,
                                        IdfPeriodValue = p.IdfPeriodValue,
                                        IdfUsers = context.project_form_reminder_users.Where(q => q.IdfProjectFormReminder == p.Id).Select(r => r.IdfUser).ToArray()
                                    }).FirstOrDefault();
        }

        public CommonResponse SaveProjectFormReminder(project_form_reminders ProjectFormReminder)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                project_form_reminders projectFormReminderFromDB = null;
                if (ProjectFormReminder.Id > 0)
                {
                    projectFormReminderFromDB = context.project_form_reminders.Where(p => p.Id == ProjectFormReminder.Id).SingleOrDefault();
                }
                if (projectFormReminderFromDB == null)
                {
                    ProjectFormReminder.Id = 0;
                    context.project_form_reminders.Add(ProjectFormReminder);
                    context.SaveChanges();
                    projectFormReminderFromDB = context.project_form_reminders.Where(p => p.Id == ProjectFormReminder.Id).Single();
                }
                else
                {
                    //projectFormReminderFromDB.IdfProjectForm = ProjectFormReminder.IdfProjectForm;
                    //projectFormReminderFromDB.IdfReminderLevel = ProjectFormReminder.IdfReminderLevel;
                    projectFormReminderFromDB.IdfPeriodType = ProjectFormReminder.IdfPeriodType;
                    projectFormReminderFromDB.IdfPeriodValue = ProjectFormReminder.IdfPeriodValue;
                    context.project_form_reminders.Update(projectFormReminderFromDB);
                    context.SaveChanges();
                }
                //foreach (project_form_field_reminders project_form_field_reminder in ProjectFormFieldReminders)
                //{
                //    project_form_field_reminder.IdfProjectFormReminder = projectFormReminderFromDB.Id;
                //    project_form_field_reminders project_form_field_reminderFromDB = context.project_form_field_reminders.Where(p => p.IdfProjectFormReminder == project_form_field_reminder.IdfProjectFormReminder && p.IdfFormField == project_form_field_reminder.IdfFormField).SingleOrDefault();
                //    if (project_form_field_reminderFromDB == null)
                //    {
                //        context.project_form_field_reminders.Add(project_form_field_reminder);
                //        context.SaveChanges();
                //    }
                //    else
                //    {
                //        project_form_field_reminderFromDB.Reminder = project_form_field_reminder.Reminder;
                //        context.project_form_field_reminders.Update(project_form_field_reminderFromDB);
                //        context.SaveChanges();
                //    }
                //}
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

        public CommonResponse DeleteProjectFormReminders(long projectFormReminderId)
        {
            CommonResponse result = new CommonResponse(); 
            var ProjectFormReminderToDelete = GetProjectFormReminderbyId(projectFormReminderId);
            if (ProjectFormReminderToDelete == null)
                result.Result = false;
            context.project_form_reminders.Remove(ProjectFormReminderToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<ProjectFormRemindersCustomEntity> GetAllProjectFormRemindersByProjectForm(long idProjectForm)
        {
            return context.project_form_reminders.Where(p=>p.IdfProjectForm==idProjectForm)
                                    .Select(p => new ProjectFormRemindersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfProjectForm = p.IdfProjectForm,
                                        IdfReminderLevel = p.IdfReminderLevel,
                                        IdfPeriodType = p.IdfPeriodType,
                                        IdfPeriodValue = p.IdfPeriodValue,
                                        IdfUsers = context.project_form_reminder_users.Where(q => q.IdfProjectFormReminder == p.Id).Select(r => r.IdfUser).ToArray()
                                    }).ToList();
        }

        public bool UpdateProjectFormReminderImage(long id, string fileName)
        {
            var newRecord = new clients_images
            {
                Name = fileName,
                IdfClient = id
            };

            context.clients_images.Add(newRecord);
            context.SaveChanges();

            context.clients.Where(c => c.Id == id).Single().IdfImg = newRecord.Id;
            context.SaveChanges();

            return true;
        }
    }
}
