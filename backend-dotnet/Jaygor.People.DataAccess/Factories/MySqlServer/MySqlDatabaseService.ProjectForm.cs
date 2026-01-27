using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Enumerators;
using JayGor.People.Entities.Responses;
using Microsoft.EntityFrameworkCore;


namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public IEnumerable<ProjectFormsCustomEntity> GetAllProjectForms()
        {
            return context.project_forms
                                    .Select(p => new ProjectFormsCustomEntity
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        Description = p.Description,
                                        Information = p.Information,
                                        Template = p.Template,
                                        TemplateFile = p.TemplateFile,
                                        IdfRecurrence = p.IdfRecurrence,
                                        IdfRecurrenceDetail = p.IdfRecurrenceDetail
                                    }).ToList();
        }
        public ProjectFormsCustomEntity GetProjectFormbyId(long id)
        {
            return context.project_forms
                        .Where(c => c.Id == id)
                                    .Select(p => new ProjectFormsCustomEntity
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        Description = p.Description,
                                        Information = p.Information,
                                        Template = p.Template,
                                        TemplateFile = p.TemplateFile,
                                        IdfRecurrence = p.IdfRecurrence,
                                        IdfRecurrenceDetail = p.IdfRecurrenceDetail
                                    }).Single();
        }

        public CommonResponse SaveProjectForm(project_forms ProjectForm)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (ProjectForm.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newProjectForm = new project_forms
                        {
                            Name = ProjectForm.Name,
                            Description = ProjectForm.Description,
                            Information = ProjectForm.Information,
                            Template = ProjectForm.Template,
                            TemplateFile = ProjectForm.TemplateFile,
                            IdfRecurrence = ProjectForm.IdfRecurrence,
                            IdfRecurrenceDetail = ProjectForm.IdfRecurrenceDetail
                        };
                        context.project_forms.Add(newProjectForm);
                        context.SaveChanges();
                        break;
                    default:
                        var project_formdb = context.project_forms.Where(c => c.Id == ProjectForm.Id).FirstOrDefault();
                        project_formdb.Name = ProjectForm.Name;
                        project_formdb.Description = ProjectForm.Description;
                        project_formdb.Information = ProjectForm.Information;
                        project_formdb.Template = ProjectForm.Template;
                        project_formdb.TemplateFile = ProjectForm.TemplateFile;
                        project_formdb.IdfRecurrence = ProjectForm.IdfRecurrence;
                        project_formdb.IdfRecurrenceDetail = ProjectForm.IdfRecurrenceDetail;
                        context.project_forms.Update(project_formdb);
                        context.SaveChanges();
                        break;
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
        public CommonResponse DeleteProjectForms(long projectFormId)
        {
            CommonResponse result = new CommonResponse();
            var ProjectFormToDelete = GetProjectFormbyId(projectFormId);
            if (ProjectFormToDelete == null)
                result.Result = false;
            context.project_forms.Remove(ProjectFormToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;

            return result;
        }
        public IEnumerable<ProjectFormbyProjectCustomEntity> GetAllProjectFormsByProject(long IdProject)
        {
            return context.project_forms.Select(p => new ProjectFormbyProjectCustomEntity
            {
                Id = p.Id,
                IdfProjectFormValue = p.Template.StartsWith("Image") ? context.project_form_image_values.Where(q => q.IdfProjectForm == p.Id && q.IdfProject == IdProject).OrderByDescending(q => q.FormDateTime).Select(q => q.Id).FirstOrDefault() : context.project_form_values.Where(q => q.IdfProjectForm == p.Id && q.IdfProject == IdProject).OrderByDescending(q => q.FormDateTime).Select(q => q.Id).FirstOrDefault(),
                Name = p.Name,
                Description = p.Description,
                Information = p.Information,
                Template = p.Template,
                TemplateFile = p.TemplateFile,
                FormDateTime = p.Template.StartsWith("Image") ? context.project_form_image_values.Where(q => q.IdfProjectForm == p.Id && q.IdfProject == IdProject).OrderByDescending(q => q.FormDateTime).Select(q => q.FormDateTime).FirstOrDefault().ToString() : context.project_form_values.Where(q => q.IdfProjectForm == p.Id && q.IdfProject == IdProject).OrderByDescending(q => q.FormDateTime).Select(q => q.FormDateTime).FirstOrDefault().ToString(),
                //NextDate = context.project_form_values.OrderByDescending(q=>q.DateTime).Where(q => q.IdfProjectForm == p.Id && q.IdfProject == IdProject).Select(q => q.FormDateTime).Max(),
                IdfRecurrence = p.IdfRecurrence,
                IdfRecurrenceDetail = p.IdfRecurrenceDetail,
                quantity = p.Template.StartsWith("Image") ? context.project_form_image_values.Where(q => q.IdfProjectForm == p.Id && q.IdfProject == IdProject).Select(q => q.Id).Count() : context.project_form_values.Where(q => q.IdfProjectForm == p.Id && q.IdfProject == IdProject).Select(q => q.Id).Count()
            }).ToList();
        }

        public IEnumerable<ProjectFormbyProjectCustomEntity> GetAllProjectFormsByProjectandProjectForm(long IdProject, long IdProjectForm)
        {
            ProjectFormsCustomEntity projectForm = GetProjectFormbyId(IdProjectForm);
            if (projectForm != null)
            {
                if (projectForm.Template.StartsWith("Image"))
                {
                    return context.project_form_image_values.Where(p => p.IdfProject == IdProject && p.IdfProjectForm == IdProjectForm).Select(p => new ProjectFormbyProjectCustomEntity
                    {
                        Id = p.Id,
                        IdfProjectFormValue = p.Id,
                        Name = projectForm.Name,
                        Description = projectForm.Description,
                        Information = projectForm.Information,
                        Template = projectForm.Template,
                        TemplateFile = projectForm.TemplateFile,
                        FormDateTime = p.FormDateTime.ToString(),
                        IdfRecurrence = projectForm.IdfRecurrence,
                        IdfRecurrenceDetail = projectForm.IdfRecurrenceDetail,
                        quantity = 1
                    }).OrderBy(p => p.FormDateTime).ToList();
                }
                else
                {
                    return context.project_form_values.Where(p => p.IdfProject == IdProject && p.IdfProjectForm == IdProjectForm).Select(p => new ProjectFormbyProjectCustomEntity
                    {
                        Id = p.Id,
                        IdfProjectFormValue = p.Id,
                        Name = projectForm.Name,
                        Description = projectForm.Description,
                        Information = projectForm.Information,
                        Template = projectForm.Template,
                        TemplateFile = projectForm.TemplateFile,
                        FormDateTime = p.FormDateTime.ToString(),
                        IdfRecurrence = projectForm.IdfRecurrence,
                        IdfRecurrenceDetail = projectForm.IdfRecurrenceDetail,
                        quantity = 1
                    }).OrderBy(p => p.FormDateTime).ToList();
                }
            }
            else
                return null;
        }

        public CommonResponse SaveProjectFormWithReminders(project_forms ProjectForm, project_form_reminders[] ProjectFormReminders, FormFieldsCustomEntity[] FormFields)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                ProjectForm.Template = ProjectForm.Template ?? "";
                project_forms projectFormFromDB = null;
                if (ProjectForm.Id > 0)
                {
                    projectFormFromDB = context.project_forms.Where(p => p.Id == ProjectForm.Id).SingleOrDefault();
                }
                if (projectFormFromDB == null)
                {
                    ProjectForm.Id = 0;
                    context.project_forms.Add(ProjectForm);
                    context.SaveChanges();
                    projectFormFromDB = context.project_forms.Where(p => p.Id == ProjectForm.Id).Single();
                }
                else
                {
                    projectFormFromDB.Name = ProjectForm.Name;
                    projectFormFromDB.Description = ProjectForm.Description;
                    projectFormFromDB.Information = ProjectForm.Information;
                    projectFormFromDB.Template = ProjectForm.Template;
                    projectFormFromDB.TemplateFile = ProjectForm.TemplateFile;
                    projectFormFromDB.IdfRecurrence = ProjectForm.IdfRecurrence;
                    projectFormFromDB.IdfRecurrenceDetail = ProjectForm.IdfRecurrenceDetail;
                    context.project_forms.Update(projectFormFromDB);
                    context.SaveChanges();
                }
                foreach (project_form_reminders project_form_reminder in ProjectFormReminders)
                {
                    project_form_reminder.IdfProjectForm = projectFormFromDB.Id;
                    project_form_reminders project_form_reminderFromDB = context.project_form_reminders.Where(p => p.IdfProjectForm == project_form_reminder.IdfProjectForm && p.IdfReminderLevel == project_form_reminder.IdfReminderLevel).FirstOrDefault();
                    if (project_form_reminderFromDB == null)
                    {
                        context.project_form_reminders.Add(project_form_reminder);
                        context.SaveChanges();
                        project_form_reminderFromDB = context.project_form_reminders.Where(p => p.IdfProjectForm == project_form_reminder.IdfProjectForm && p.IdfReminderLevel == project_form_reminder.IdfReminderLevel).FirstOrDefault();
                    }
                    else
                    {
                        //project_form_reminderFromDB.IdfReminderLevel = project_form_reminder.IdfReminderLevel;
                        project_form_reminderFromDB.IdfPeriodType = project_form_reminder.IdfPeriodType;
                        project_form_reminderFromDB.IdfPeriodValue = project_form_reminder.IdfPeriodValue;
                        context.project_form_reminders.Update(project_form_reminderFromDB);
                        context.SaveChanges();
                    }
                    project_form_reminder_users[] project_form_reminder_userForDelete = context.project_form_reminder_users.Where(p => p.IdfProjectFormReminder == project_form_reminderFromDB.Id && !project_form_reminder.IdfUsers.Contains(p.IdfUser)).ToArray();
                    context.project_form_reminder_users.RemoveRange(project_form_reminder_userForDelete);
                    context.SaveChanges();

                    foreach (long idfuser in project_form_reminder.IdfUsers)
                    {
                        project_form_reminder_users project_form_reminder_userFromDB = context.project_form_reminder_users.Where(p => p.IdfProjectFormReminder == project_form_reminderFromDB.Id && p.IdfUser == idfuser).FirstOrDefault();
                        if (project_form_reminder_userFromDB == null)
                        {
                            project_form_reminder_users project_form_reminder_user = new project_form_reminder_users();
                            project_form_reminder_user.IdfProjectFormReminder = project_form_reminderFromDB.Id;
                            project_form_reminder_user.IdfUser = idfuser;
                            context.project_form_reminder_users.Add(project_form_reminder_user);
                            context.SaveChanges();
                        }
                    }
                }
                foreach (FormFieldsCustomEntity customFormField in FormFields)
                {
                    project_form_fields project_form_fieldFromDB = context.project_form_fields.Where(p => p.IdfProjectForm == ProjectForm.Id && p.IdfFormField == customFormField.Id).FirstOrDefault();
                    if (project_form_fieldFromDB == null)
                    {
                        form_fields formField = new form_fields();
                        formField.Name = customFormField.Name;
                        formField.Description = customFormField.Description;
                        formField.Placeholder = customFormField.Placeholder;
                        formField.Datatype = customFormField.Datatype;
                        formField.Constraints = customFormField.Constraints;
                        context.form_fields.Add(formField);
                        context.SaveChanges();
                        project_form_fields projectFormField = new project_form_fields();
                        projectFormField.IdfFormField = formField.Id;
                        projectFormField.IdfProjectForm = ProjectForm.Id;
                        projectFormField.Position = customFormField.Position;
                        projectFormField.IsEnabled = customFormField.IsEnabled;
                        context.project_form_fields.Add(projectFormField);
                        context.SaveChanges();
                    }
                    else
                    {
                        form_fields form_fieldFromDB = context.form_fields.Where(p => p.Id == project_form_fieldFromDB.IdfFormField).FirstOrDefault();
                        form_fieldFromDB.Placeholder = customFormField.Placeholder;
                        form_fieldFromDB.Description = customFormField.Description;
                        context.form_fields.Update(form_fieldFromDB);
                        context.SaveChanges();
                        project_form_fieldFromDB.Position = customFormField.Position;
                        project_form_fieldFromDB.IsEnabled = customFormField.IsEnabled;
                        context.project_form_fields.Update(project_form_fieldFromDB);
                        context.SaveChanges();
                    }
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
    }
}
