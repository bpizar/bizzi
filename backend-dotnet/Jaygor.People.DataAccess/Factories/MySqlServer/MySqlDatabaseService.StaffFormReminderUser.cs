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
        public IEnumerable<StaffFormReminderUsersCustomEntity> GetAllStaffFormReminderUsers()
        {
            return context.staff_form_reminder_users
                                    .Select(p => new StaffFormReminderUsersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffFormReminder = p.IdfStaffFormReminder,
                                        IdfUser = p.IdfUser,
                                    }).ToList();
        }

        public StaffFormReminderUsersCustomEntity GetStaffFormReminderUserbyId(long id)
        {
            return context.staff_form_reminder_users
                        .Where(c => c.Id == id)
                                    .Select(p => new StaffFormReminderUsersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffFormReminder = p.IdfStaffFormReminder,
                                        IdfUser = p.IdfUser
                                    }).FirstOrDefault();
        }

        public CommonResponse SaveStaffFormReminderUser(staff_form_reminder_users StaffFormReminderUser)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                staff_form_reminder_users staffFormReminderUserFromDB = null;
                if (StaffFormReminderUser.Id > 0)
                {
                    staffFormReminderUserFromDB = context.staff_form_reminder_users.Where(p => p.Id == StaffFormReminderUser.Id).SingleOrDefault();
                }
                if (staffFormReminderUserFromDB == null)
                {
                    StaffFormReminderUser.Id = 0;
                    context.staff_form_reminder_users.Add(StaffFormReminderUser);
                    context.SaveChanges();
                    staffFormReminderUserFromDB = context.staff_form_reminder_users.Where(p => p.Id == StaffFormReminderUser.Id).Single();
                }
                else
                {
                    staffFormReminderUserFromDB.IdfStaffFormReminder = StaffFormReminderUser.IdfStaffFormReminder;
                    staffFormReminderUserFromDB.IdfUser = StaffFormReminderUser.IdfUser;
                    context.staff_form_reminder_users.Update(staffFormReminderUserFromDB);
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

        public CommonResponse DeleteStaffFormReminderUsers(long staffFormReminderUserId)
        {
            CommonResponse result = new CommonResponse(); 
            var StaffFormReminderUserToDelete = GetStaffFormReminderUserbyId(staffFormReminderUserId);
            if (StaffFormReminderUserToDelete == null)
                result.Result = false;
            context.staff_form_reminder_users.Remove(StaffFormReminderUserToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<StaffFormReminderUsersCustomEntity> GetAllStaffFormReminderUsersByStaffFormReminder(long idStaffFormReminder)
        {
            return context.staff_form_reminder_users.Where(p=>p.IdfStaffFormReminder== idStaffFormReminder)
                                    .Select(p => new StaffFormReminderUsersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffFormReminder = p.IdfStaffFormReminder,
                                        IdfUser = p.IdfUser,
                                    }).ToList();
        }

    }
}
