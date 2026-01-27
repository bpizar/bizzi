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
        public IEnumerable<ClientFormReminderUsersCustomEntity> GetAllClientFormReminderUsers()
        {
            return context.client_form_reminder_users
                                    .Select(p => new ClientFormReminderUsersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientFormReminder = p.IdfClientFormReminder,
                                        IdfUser = p.IdfUser,
                                    }).ToList();
        }

        public ClientFormReminderUsersCustomEntity GetClientFormReminderUserbyId(long id)
        {
            return context.client_form_reminder_users
                        .Where(c => c.Id == id)
                                    .Select(p => new ClientFormReminderUsersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientFormReminder = p.IdfClientFormReminder,
                                        IdfUser = p.IdfUser
                                    }).FirstOrDefault();
        }

        public CommonResponse SaveClientFormReminderUser(client_form_reminder_users ClientFormReminderUser)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                client_form_reminder_users clientFormReminderUserFromDB = null;
                if (ClientFormReminderUser.Id > 0)
                {
                    clientFormReminderUserFromDB = context.client_form_reminder_users.Where(p => p.Id == ClientFormReminderUser.Id).SingleOrDefault();
                }
                if (clientFormReminderUserFromDB == null)
                {
                    ClientFormReminderUser.Id = 0;
                    context.client_form_reminder_users.Add(ClientFormReminderUser);
                    context.SaveChanges();
                    clientFormReminderUserFromDB = context.client_form_reminder_users.Where(p => p.Id == ClientFormReminderUser.Id).Single();
                }
                else
                {
                    clientFormReminderUserFromDB.IdfClientFormReminder = ClientFormReminderUser.IdfClientFormReminder;
                    clientFormReminderUserFromDB.IdfUser = ClientFormReminderUser.IdfUser;
                    context.client_form_reminder_users.Update(clientFormReminderUserFromDB);
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

        public CommonResponse DeleteClientFormReminderUsers(long clientFormReminderUserId)
        {
            CommonResponse result = new CommonResponse(); 
            var ClientFormReminderUserToDelete = GetClientFormReminderUserbyId(clientFormReminderUserId);
            if (ClientFormReminderUserToDelete == null)
                result.Result = false;
            context.client_form_reminder_users.Remove(ClientFormReminderUserToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<ClientFormReminderUsersCustomEntity> GetAllClientFormReminderUsersByClientFormReminder(long idClientFormReminder)
        {
            return context.client_form_reminder_users.Where(p=>p.IdfClientFormReminder== idClientFormReminder)
                                    .Select(p => new ClientFormReminderUsersCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientFormReminder = p.IdfClientFormReminder,
                                        IdfUser = p.IdfUser,
                                    }).ToList();
        }

    }
}
