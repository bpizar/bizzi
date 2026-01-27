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
        public IEnumerable<ClientFormRemindersCustomEntity> GetAllClientFormReminders()
        {
            return context.client_form_reminders
                                    .Select(p => new ClientFormRemindersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfReminderLevel = p.IdfReminderLevel,
                                        IdfPeriodType = p.IdfPeriodType,
                                        IdfPeriodValue = p.IdfPeriodValue,
                                        IdfUsers = context.client_form_reminder_users.Where(q => q.IdfClientFormReminder == p.Id).Select(r => r.IdfUser).ToArray()
                                    }).ToList();
        }

        public ClientFormRemindersCustomEntity GetClientFormReminderbyId(long id)
        {
            return context.client_form_reminders
                        .Where(c => c.Id == id)
                                    .Select(p => new ClientFormRemindersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfReminderLevel = p.IdfReminderLevel,
                                        IdfPeriodType = p.IdfPeriodType,
                                        IdfPeriodValue = p.IdfPeriodValue,
                                        IdfUsers = context.client_form_reminder_users.Where(q => q.IdfClientFormReminder == p.Id).Select(r => r.IdfUser).ToArray()
                                    }).FirstOrDefault();
        }

        public CommonResponse SaveClientFormReminder(client_form_reminders ClientFormReminder)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                client_form_reminders clientFormReminderFromDB = null;
                if (ClientFormReminder.Id > 0)
                {
                    clientFormReminderFromDB = context.client_form_reminders.Where(p => p.Id == ClientFormReminder.Id).SingleOrDefault();
                }
                if (clientFormReminderFromDB == null)
                {
                    ClientFormReminder.Id = 0;
                    context.client_form_reminders.Add(ClientFormReminder);
                    context.SaveChanges();
                    clientFormReminderFromDB = context.client_form_reminders.Where(p => p.Id == ClientFormReminder.Id).Single();
                }
                else
                {
                    //clientFormReminderFromDB.IdfClientForm = ClientFormReminder.IdfClientForm;
                    //clientFormReminderFromDB.IdfReminderLevel = ClientFormReminder.IdfReminderLevel;
                    clientFormReminderFromDB.IdfPeriodType = ClientFormReminder.IdfPeriodType;
                    clientFormReminderFromDB.IdfPeriodValue = ClientFormReminder.IdfPeriodValue;
                    context.client_form_reminders.Update(clientFormReminderFromDB);
                    context.SaveChanges();
                }
                //foreach (client_form_field_reminders client_form_field_reminder in ClientFormFieldReminders)
                //{
                //    client_form_field_reminder.IdfClientFormReminder = clientFormReminderFromDB.Id;
                //    client_form_field_reminders client_form_field_reminderFromDB = context.client_form_field_reminders.Where(p => p.IdfClientFormReminder == client_form_field_reminder.IdfClientFormReminder && p.IdfFormField == client_form_field_reminder.IdfFormField).SingleOrDefault();
                //    if (client_form_field_reminderFromDB == null)
                //    {
                //        context.client_form_field_reminders.Add(client_form_field_reminder);
                //        context.SaveChanges();
                //    }
                //    else
                //    {
                //        client_form_field_reminderFromDB.Reminder = client_form_field_reminder.Reminder;
                //        context.client_form_field_reminders.Update(client_form_field_reminderFromDB);
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

        public CommonResponse DeleteClientFormReminders(long clientFormReminderId)
        {
            CommonResponse result = new CommonResponse(); 
            var ClientFormReminderToDelete = GetClientFormReminderbyId(clientFormReminderId);
            if (ClientFormReminderToDelete == null)
                result.Result = false;
            context.client_form_reminders.Remove(ClientFormReminderToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<ClientFormRemindersCustomEntity> GetAllClientFormRemindersByClientForm(long idClientForm)
        {
            return context.client_form_reminders.Where(p=>p.IdfClientForm==idClientForm)
                                    .Select(p => new ClientFormRemindersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfReminderLevel = p.IdfReminderLevel,
                                        IdfPeriodType = p.IdfPeriodType,
                                        IdfPeriodValue = p.IdfPeriodValue,
                                        IdfUsers = context.client_form_reminder_users.Where(q => q.IdfClientFormReminder == p.Id).Select(r => r.IdfUser).ToArray()
                                    }).ToList();
        }

        public bool UpdateClientFormReminderImage(long id, string fileName)
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
