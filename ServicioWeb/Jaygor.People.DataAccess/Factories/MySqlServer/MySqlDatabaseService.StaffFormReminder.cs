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
        public IEnumerable<StaffFormRemindersCustomEntity> GetAllStaffFormReminders()
        {
            return context.staff_form_reminders
                                    .Select(p => new StaffFormRemindersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfReminderLevel = p.IdfReminderLevel,
                                        IdfPeriodType = p.IdfPeriodType,
                                        IdfPeriodValue = p.IdfPeriodValue,
                                        IdfUsers = context.staff_form_reminder_users.Where(q => q.IdfStaffFormReminder == p.Id).Select(r => r.IdfUser).ToArray()
                                    }).ToList();
        }

        public StaffFormRemindersCustomEntity GetStaffFormReminderbyId(long id)
        {
            return context.staff_form_reminders
                        .Where(c => c.Id == id)
                                    .Select(p => new StaffFormRemindersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfReminderLevel = p.IdfReminderLevel,
                                        IdfPeriodType = p.IdfPeriodType,
                                        IdfPeriodValue = p.IdfPeriodValue,
                                        IdfUsers = context.staff_form_reminder_users.Where(q => q.IdfStaffFormReminder == p.Id).Select(r => r.IdfUser).ToArray()
                                    }).FirstOrDefault();
        }

        public CommonResponse SaveStaffFormReminder(staff_form_reminders StaffFormReminder)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                staff_form_reminders staffFormReminderFromDB = null;
                if (StaffFormReminder.Id > 0)
                {
                    staffFormReminderFromDB = context.staff_form_reminders.Where(p => p.Id == StaffFormReminder.Id).SingleOrDefault();
                }
                if (staffFormReminderFromDB == null)
                {
                    StaffFormReminder.Id = 0;
                    context.staff_form_reminders.Add(StaffFormReminder);
                    context.SaveChanges();
                    staffFormReminderFromDB = context.staff_form_reminders.Where(p => p.Id == StaffFormReminder.Id).Single();
                }
                else
                {
                    //staffFormReminderFromDB.IdfStaffForm = StaffFormReminder.IdfStaffForm;
                    //staffFormReminderFromDB.IdfReminderLevel = StaffFormReminder.IdfReminderLevel;
                    staffFormReminderFromDB.IdfPeriodType = StaffFormReminder.IdfPeriodType;
                    staffFormReminderFromDB.IdfPeriodValue = StaffFormReminder.IdfPeriodValue;
                    context.staff_form_reminders.Update(staffFormReminderFromDB);
                    context.SaveChanges();
                }
                //foreach (staff_form_field_reminders staff_form_field_reminder in StaffFormFieldReminders)
                //{
                //    staff_form_field_reminder.IdfStaffFormReminder = staffFormReminderFromDB.Id;
                //    staff_form_field_reminders staff_form_field_reminderFromDB = context.staff_form_field_reminders.Where(p => p.IdfStaffFormReminder == staff_form_field_reminder.IdfStaffFormReminder && p.IdfFormField == staff_form_field_reminder.IdfFormField).SingleOrDefault();
                //    if (staff_form_field_reminderFromDB == null)
                //    {
                //        context.staff_form_field_reminders.Add(staff_form_field_reminder);
                //        context.SaveChanges();
                //    }
                //    else
                //    {
                //        staff_form_field_reminderFromDB.Reminder = staff_form_field_reminder.Reminder;
                //        context.staff_form_field_reminders.Update(staff_form_field_reminderFromDB);
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

        public CommonResponse DeleteStaffFormReminders(long staffFormReminderId)
        {
            CommonResponse result = new CommonResponse(); 
            var StaffFormReminderToDelete = GetStaffFormReminderbyId(staffFormReminderId);
            if (StaffFormReminderToDelete == null)
                result.Result = false;
            context.staff_form_reminders.Remove(StaffFormReminderToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<StaffFormRemindersCustomEntity> GetAllStaffFormRemindersByStaffForm(long idStaffForm)
        {
            return context.staff_form_reminders.Where(p=>p.IdfStaffForm==idStaffForm)
                                    .Select(p => new StaffFormRemindersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfReminderLevel = p.IdfReminderLevel,
                                        IdfPeriodType = p.IdfPeriodType,
                                        IdfPeriodValue = p.IdfPeriodValue,
                                        IdfUsers = context.staff_form_reminder_users.Where(q => q.IdfStaffFormReminder == p.Id).Select(r => r.IdfUser).ToArray()
                                    }).ToList();
        }

        public bool UpdateStaffFormReminderImage(long id, string fileName)
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
