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
        public IEnumerable<ClientFormValuesCustomEntity> GetAllClientFormValues()
        {
            return context.client_form_values
                                    .Select(p => new ClientFormValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfClient = p.IdfClient,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime,
                                    }).ToList();
        }

        public ClientFormValuesCustomEntity GetClientFormValuebyId(long id)
        {
            return context.client_form_values
                        .Where(c => c.Id == id)
                                    .Select(p => new ClientFormValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfClient = p.IdfClient,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime
                                    }).Single();
        }

        public CommonResponse SaveClientFormValue(client_form_values ClientFormValue)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (ClientFormValue.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newClientFormValue = new client_form_values
                        {
                            IdfClientForm = ClientFormValue.IdfClientForm,
                            IdfClient = ClientFormValue.IdfClient,
                            FormDateTime = ClientFormValue.FormDateTime,
                            DateTime = DateTime.Now
                        };
                        context.client_form_values.Add(newClientFormValue);
                        context.SaveChanges();
                        break;
                    default:
                        var client_form_valuedb = context.client_form_values.Where(c => c.Id == ClientFormValue.Id).FirstOrDefault();
                        client_form_valuedb.IdfClientForm = ClientFormValue.IdfClientForm;
                        client_form_valuedb.IdfClient = ClientFormValue.IdfClient;
                        client_form_valuedb.FormDateTime = ClientFormValue.FormDateTime;
                        client_form_valuedb.DateTime = DateTime.Now;
                        context.client_form_values.Update(client_form_valuedb);
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

        public CommonResponse DeleteClientFormValues(long clientFormValueId)
        {
            CommonResponse result = new CommonResponse(); 
            var ClientFormValueToDelete = GetClientFormValuebyId(clientFormValueId);
            if (ClientFormValueToDelete == null)
                result.Result = false;
            context.client_form_values.Remove(ClientFormValueToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<ClientFormValuesCustomEntity> GetAllClientFormValuesByClientFormAndClient(long idClientForm, long idClient)
        {
            return context.client_form_values.Where(p=>p.IdfClient==idClient && p.IdfClientForm==idClientForm)
                                    .Select(p => new ClientFormValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfClient = p.IdfClient,
                                        DateTime = p.DateTime,
                                        FormDateTime = p.FormDateTime,
                                    }).ToList();
        }

        public CommonResponse SaveClientFormValueWithDetail(client_form_values ClientFormValue, client_form_field_values[] ClientFormFieldValues)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                client_form_values clientFormValueFromDB = null;
                if (ClientFormValue.Id>0)
                {
                    clientFormValueFromDB = context.client_form_values.Where(p => p.Id == ClientFormValue.Id).SingleOrDefault();
                }
                if (clientFormValueFromDB==null)
                {
                    ClientFormValue.Id = 0;
                    ClientFormValue.DateTime = DateTime.Now;
                    context.client_form_values.Add(ClientFormValue);
                    context.SaveChanges();  
                    clientFormValueFromDB = context.client_form_values.Where(p => p.Id == ClientFormValue.Id).Single();
                }
                foreach (client_form_field_values client_form_field_value in ClientFormFieldValues)
                {
                    client_form_field_value.IdfClientFormValue = clientFormValueFromDB.Id;
                    client_form_field_values client_form_field_valueFromDB = context.client_form_field_values.Where(p => p.IdfClientFormValue == client_form_field_value.IdfClientFormValue && p.IdfFormField == client_form_field_value.IdfFormField).SingleOrDefault();
                    if (client_form_field_valueFromDB == null)
                    {
                        context.client_form_field_values.Add(client_form_field_value);
                        context.SaveChanges();
                    }
                    else
                    {
                        client_form_field_valueFromDB.Value = client_form_field_value.Value;
                        context.client_form_field_values.Update(client_form_field_valueFromDB);
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
        public bool UpdateClientFormValueImage(long id, string fileName)
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
