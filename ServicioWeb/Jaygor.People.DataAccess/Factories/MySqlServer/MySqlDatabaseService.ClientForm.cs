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
        public IEnumerable<ClientFormsCustomEntity> GetAllClientForms()
        {
            return context.client_forms
                                    .Select(p => new ClientFormsCustomEntity
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
        public ClientFormsCustomEntity GetClientFormbyId(long id)
        {
            return context.client_forms
                        .Where(c => c.Id == id)
                                    .Select(p => new ClientFormsCustomEntity
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

        public CommonResponse SaveClientForm(client_forms ClientForm)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (ClientForm.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newClientForm = new client_forms
                        {
                            Name = ClientForm.Name,
                            Description = ClientForm.Description,
                            Information = ClientForm.Information,
                            Template = ClientForm.Template,
                            TemplateFile = ClientForm.TemplateFile,
                            IdfRecurrence = ClientForm.IdfRecurrence,
                            IdfRecurrenceDetail = ClientForm.IdfRecurrenceDetail
                        };
                        context.client_forms.Add(newClientForm);
                        context.SaveChanges();
                        break;
                    default:
                        var client_formdb = context.client_forms.Where(c => c.Id == ClientForm.Id).FirstOrDefault();
                        client_formdb.Name = ClientForm.Name;
                        client_formdb.Description = ClientForm.Description;
                        client_formdb.Information = ClientForm.Information;
                        client_formdb.Template = ClientForm.Template;
                        client_formdb.TemplateFile = ClientForm.TemplateFile;
                        client_formdb.IdfRecurrence = ClientForm.IdfRecurrence;
                        client_formdb.IdfRecurrenceDetail = ClientForm.IdfRecurrenceDetail;
                        context.client_forms.Update(client_formdb);
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
        public CommonResponse DeleteClientForms(long clientFormId)
        {
            CommonResponse result = new CommonResponse();
            var ClientFormToDelete = GetClientFormbyId(clientFormId);
            if (ClientFormToDelete == null)
                result.Result = false;
            context.client_forms.Remove(ClientFormToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;

            return result;
        }
        public IEnumerable<ClientFormbyClientCustomEntity> GetAllClientFormsByClient(long IdClient)
        {
            return context.client_forms.Select(p => new ClientFormbyClientCustomEntity
            {
                Id = p.Id,
                IdfClientFormValue = p.Template.StartsWith("Image") ? context.client_form_image_values.Where(q => q.IdfClientForm == p.Id && q.IdfClient == IdClient).OrderByDescending(q => q.FormDateTime).Select(q => q.Id).FirstOrDefault() : context.client_form_values.Where(q => q.IdfClientForm == p.Id && q.IdfClient == IdClient).OrderByDescending(q => q.FormDateTime).Select(q => q.Id).FirstOrDefault(),
                Name = p.Name,
                Description = p.Description,
                Information = p.Information,
                Template = p.Template,
                TemplateFile = p.TemplateFile,
                FormDateTime = p.Template.StartsWith("Image") ? context.client_form_image_values.Where(q => q.IdfClientForm == p.Id && q.IdfClient == IdClient).OrderByDescending(q => q.FormDateTime).Select(q => q.FormDateTime).FirstOrDefault().ToString() : context.client_form_values.Where(q => q.IdfClientForm == p.Id && q.IdfClient == IdClient).OrderByDescending(q => q.FormDateTime).Select(q => q.FormDateTime).FirstOrDefault().ToString(),
                //NextDate = context.client_form_values.OrderByDescending(q=>q.DateTime).Where(q => q.IdfClientForm == p.Id && q.IdfClient == IdClient).Select(q => q.FormDateTime).Max(),
                IdfRecurrence = p.IdfRecurrence,
                IdfRecurrenceDetail = p.IdfRecurrenceDetail,
                quantity = p.Template.StartsWith("Image") ? context.client_form_image_values.Where(q => q.IdfClientForm == p.Id && q.IdfClient == IdClient).Select(q => q.Id).Count() : context.client_form_values.Where(q => q.IdfClientForm == p.Id && q.IdfClient == IdClient).Select(q => q.Id).Count()
            }).ToList();
        }

        public IEnumerable<ClientFormbyClientCustomEntity> GetAllClientFormsByClientandClientForm(long IdClient, long IdClientForm)
        {
            ClientFormsCustomEntity clientForm = GetClientFormbyId(IdClientForm);
            if (clientForm != null)
            {
                if (clientForm.Template.StartsWith("Image"))
                {
                    return context.client_form_image_values.Where(p => p.IdfClient == IdClient && p.IdfClientForm == IdClientForm).Select(p => new ClientFormbyClientCustomEntity
                    {
                        Id = p.Id,
                        IdfClientFormValue = p.Id,
                        Name = clientForm.Name,
                        Description = clientForm.Description,
                        Information = clientForm.Information,
                        Template = clientForm.Template,
                        TemplateFile = clientForm.TemplateFile,
                        FormDateTime = p.FormDateTime.ToString(),
                        IdfRecurrence = clientForm.IdfRecurrence,
                        IdfRecurrenceDetail = clientForm.IdfRecurrenceDetail,
                        quantity = 1
                    }).OrderBy(p => p.FormDateTime).ToList();
                }
                else
                {
                    return context.client_form_values.Where(p => p.IdfClient == IdClient && p.IdfClientForm == IdClientForm).Select(p => new ClientFormbyClientCustomEntity
                    {
                        Id = p.Id,
                        IdfClientFormValue = p.Id,
                        Name = clientForm.Name,
                        Description = clientForm.Description,
                        Information = clientForm.Information,
                        Template = clientForm.Template,
                        TemplateFile = clientForm.TemplateFile,
                        FormDateTime = p.FormDateTime.ToString(),
                        IdfRecurrence = clientForm.IdfRecurrence,
                        IdfRecurrenceDetail = clientForm.IdfRecurrenceDetail,
                        quantity = 1
                    }).OrderBy(p => p.FormDateTime).ToList();
                }
            }
            else
                return null;
        }

        public CommonResponse SaveClientFormWithReminders(client_forms ClientForm, client_form_reminders[] ClientFormReminders, FormFieldsCustomEntity[] FormFields)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                ClientForm.Template = ClientForm.Template ?? "";
                client_forms clientFormFromDB = null;
                if (ClientForm.Id > 0)
                {
                    clientFormFromDB = context.client_forms.Where(p => p.Id == ClientForm.Id).SingleOrDefault();
                }
                if (clientFormFromDB == null)
                {
                    ClientForm.Id = 0;
                    context.client_forms.Add(ClientForm);
                    context.SaveChanges();
                    clientFormFromDB = context.client_forms.Where(p => p.Id == ClientForm.Id).Single();
                }
                else
                {
                    clientFormFromDB.Name = ClientForm.Name;
                    clientFormFromDB.Description = ClientForm.Description;
                    clientFormFromDB.Information = ClientForm.Information;
                    clientFormFromDB.Template = ClientForm.Template;
                    clientFormFromDB.TemplateFile = ClientForm.TemplateFile;
                    clientFormFromDB.IdfRecurrence = ClientForm.IdfRecurrence;
                    clientFormFromDB.IdfRecurrenceDetail = ClientForm.IdfRecurrenceDetail;
                    context.client_forms.Update(clientFormFromDB);
                    context.SaveChanges();
                }
                foreach (client_form_reminders client_form_reminder in ClientFormReminders)
                {
                    client_form_reminder.IdfClientForm = clientFormFromDB.Id;
                    client_form_reminders client_form_reminderFromDB = context.client_form_reminders.Where(p => p.IdfClientForm == client_form_reminder.IdfClientForm && p.IdfReminderLevel == client_form_reminder.IdfReminderLevel).FirstOrDefault();
                    if (client_form_reminderFromDB == null)
                    {
                        context.client_form_reminders.Add(client_form_reminder);
                        context.SaveChanges();
                        client_form_reminderFromDB = context.client_form_reminders.Where(p => p.IdfClientForm == client_form_reminder.IdfClientForm && p.IdfReminderLevel == client_form_reminder.IdfReminderLevel).FirstOrDefault();
                    }
                    else
                    {
                        //client_form_reminderFromDB.IdfReminderLevel = client_form_reminder.IdfReminderLevel;
                        client_form_reminderFromDB.IdfPeriodType = client_form_reminder.IdfPeriodType;
                        client_form_reminderFromDB.IdfPeriodValue = client_form_reminder.IdfPeriodValue;
                        context.client_form_reminders.Update(client_form_reminderFromDB);
                        context.SaveChanges();
                    }
                    client_form_reminder_users[] client_form_reminder_userForDelete = context.client_form_reminder_users.Where(p => p.IdfClientFormReminder == client_form_reminderFromDB.Id && !client_form_reminder.IdfUsers.Contains(p.IdfUser)).ToArray();
                    context.client_form_reminder_users.RemoveRange(client_form_reminder_userForDelete);
                    context.SaveChanges();

                    foreach (long idfuser in client_form_reminder.IdfUsers)
                    {
                        client_form_reminder_users client_form_reminder_userFromDB = context.client_form_reminder_users.Where(p => p.IdfClientFormReminder == client_form_reminderFromDB.Id && p.IdfUser == idfuser).FirstOrDefault();
                        if (client_form_reminder_userFromDB == null)
                        {
                            client_form_reminder_users client_form_reminder_user = new client_form_reminder_users();
                            client_form_reminder_user.IdfClientFormReminder = client_form_reminderFromDB.Id;
                            client_form_reminder_user.IdfUser = idfuser;
                            context.client_form_reminder_users.Add(client_form_reminder_user);
                            context.SaveChanges();
                        }
                    }
                }
                foreach (FormFieldsCustomEntity customFormField in FormFields)
                {
                    client_form_fields client_form_fieldFromDB = context.client_form_fields.Where(p => p.IdfClientForm == ClientForm.Id && p.IdfFormField == customFormField.Id).FirstOrDefault();
                    if (client_form_fieldFromDB == null)
                    {
                        form_fields formField = new form_fields();
                        formField.Name = customFormField.Name;
                        formField.Description = customFormField.Description;
                        formField.Placeholder = customFormField.Placeholder;
                        formField.Datatype = customFormField.Datatype;
                        formField.Constraints = customFormField.Constraints;
                        context.form_fields.Add(formField);
                        context.SaveChanges();
                        client_form_fields clientFormField = new client_form_fields();
                        clientFormField.IdfFormField = formField.Id;
                        clientFormField.IdfClientForm = ClientForm.Id;
                        clientFormField.Position = customFormField.Position;
                        clientFormField.IsEnabled = customFormField.IsEnabled;
                        context.client_form_fields.Add(clientFormField);
                        context.SaveChanges();
                    }
                    else
                    {
                        form_fields form_fieldFromDB = context.form_fields.Where(p => p.Id == client_form_fieldFromDB.IdfFormField).FirstOrDefault();
                        form_fieldFromDB.Placeholder = customFormField.Placeholder;
                        form_fieldFromDB.Description = customFormField.Description;
                        context.form_fields.Update(form_fieldFromDB);
                        context.SaveChanges();
                        client_form_fieldFromDB.Position = customFormField.Position;
                        client_form_fieldFromDB.IsEnabled = customFormField.IsEnabled;
                        context.client_form_fields.Update(client_form_fieldFromDB);
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
