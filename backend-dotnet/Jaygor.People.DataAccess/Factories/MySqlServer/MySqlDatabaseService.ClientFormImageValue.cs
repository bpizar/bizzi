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
        public IEnumerable<ClientFormImageValuesCustomEntity> GetAllClientFormImageValues()
        {
            return context.client_form_image_values
                                    .Select(p => new ClientFormImageValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfClient = p.IdfClient,
                                        Image = p.Image,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime,
                                    }).ToList();
        }

        public ClientFormImageValuesCustomEntity GetClientFormImageValuebyId(long id)
        {
            return context.client_form_image_values
                        .Where(c => c.Id == id)
                                    .Select(p => new ClientFormImageValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfClient = p.IdfClient,
                                        Image = p.Image,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime
                                    }).Single();
        }

        public CommonResponse SaveClientFormImageValue(client_form_image_values ClientFormImageValue)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                client_form_image_values clientFormImageValueFromDB = null;
                if (ClientFormImageValue.Id > 0)
                {
                    clientFormImageValueFromDB = context.client_form_image_values.Where(p => p.Id == ClientFormImageValue.Id).SingleOrDefault();
                }
                if (clientFormImageValueFromDB is null)
                {
                    ClientFormImageValue.Id = 0;
                    ClientFormImageValue.DateTime = DateTime.Now;
                    context.client_form_image_values.Add(ClientFormImageValue);
                    context.SaveChanges();
                    clientFormImageValueFromDB = context.client_form_image_values.Where(p => p.Id == ClientFormImageValue.Id).Single();
                }
                else
                {
                    clientFormImageValueFromDB.Image = ClientFormImageValue.Image ?? clientFormImageValueFromDB.Image;
                    clientFormImageValueFromDB.FormDateTime = ClientFormImageValue.FormDateTime;
                    clientFormImageValueFromDB.DateTime = DateTime.Now;
                    context.client_form_image_values.Update(clientFormImageValueFromDB);
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

        public CommonResponse DeleteClientFormImageValues(long clientFormImageValueId)
        {
            CommonResponse result = new CommonResponse(); 
            var ClientFormImageValueToDelete = GetClientFormImageValuebyId(clientFormImageValueId);
            if (ClientFormImageValueToDelete == null)
                result.Result = false;
            context.client_form_image_values.Remove(ClientFormImageValueToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public ClientFormImageValuesCustomEntity GetClientFormImageValueByClientFormAndClient(long idClientForm, long idClient)
        {
            return context.client_form_image_values.Where(p=>p.IdfClient==idClient && p.IdfClientForm==idClientForm)
                                    .Select(p => new ClientFormImageValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfClient = p.IdfClient,
                                        Image = p.Image,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime
                                    }).FirstOrDefault();
        }   
    }
}
