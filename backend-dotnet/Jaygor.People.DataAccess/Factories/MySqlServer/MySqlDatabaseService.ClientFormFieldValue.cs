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
        public IEnumerable<ClientFormFieldValuesCustomEntity> GetAllClientFormFieldValues()
        {
            return context.client_form_field_values
                                    .Select(p => new ClientFormFieldValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientFormValue = p.IdfClientFormValue,
                                        IdfFormField = p.IdfFormField,
                                        Value = p.Value,
                                    }).ToList();
        }

        public ClientFormFieldValuesCustomEntity GetClientFormFieldValuebyId(long id)
        {
            return context.client_form_field_values
                        .Where(c => c.Id == id)
                                    .Select(p => new ClientFormFieldValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientFormValue = p.IdfClientFormValue,
                                        IdfFormField = p.IdfFormField,
                                        Value = p.Value,
                                    }).Single();
        }

        public CommonResponse SaveClientFormFieldValue(client_form_field_values ClientFormFieldValue)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (ClientFormFieldValue.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newClientFormFieldValue = new client_form_field_values
                        {
                            IdfClientFormValue = ClientFormFieldValue.IdfClientFormValue,
                            IdfFormField = ClientFormFieldValue.IdfFormField,
                            Value = ClientFormFieldValue.Value,
                        };
                        context.client_form_field_values.Add(newClientFormFieldValue);
                        context.SaveChanges();
                        break;
                    default:
                        var client_form_field_valuedb = context.client_form_field_values.Where(c => c.Id == ClientFormFieldValue.Id).FirstOrDefault();
                        client_form_field_valuedb.IdfClientFormValue = ClientFormFieldValue.IdfClientFormValue;
                        client_form_field_valuedb.IdfFormField = ClientFormFieldValue.IdfFormField;
                        client_form_field_valuedb.Value = ClientFormFieldValue.Value;
                        context.client_form_field_values.Update(client_form_field_valuedb);
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

        public CommonResponse DeleteClientFormFieldValues(long clientFormFieldValueId)
        {
            CommonResponse result = new CommonResponse(); 
            var ClientFormFieldValueToDelete = GetClientFormFieldValuebyId(clientFormFieldValueId);
            if (ClientFormFieldValueToDelete == null)
                result.Result = false;
            context.client_form_field_values.Remove(ClientFormFieldValueToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<ClientFormFieldValuesCustomEntity> GetAllClientFormFieldValuesByClientFormAndClient(long idClientForm, long idClient)
        {
            return context.client_form_field_values
                .Join(context.client_form_values,
                                          sffv => sffv.IdfClientFormValue,
                                          sfv => sfv.Id,
                                          (sffv, sfv) => new { sffv, sfv })
                .Where(p => p.sfv.IdfClient == idClient && p.sfv.IdfClientForm == idClientForm)
                                    .Select(p => new ClientFormFieldValuesCustomEntity
                                    {
                                        Id = p.sffv.Id,
                                        IdfClientFormValue = p.sffv.IdfClientFormValue,
                                        IdfFormField = p.sffv.IdfFormField,
                                        Value = p.sffv.Value,
                                    }).ToList();
        }

        public IEnumerable<ClientFormFieldValuesCustomEntity> GetAllClientFormFieldValuesByClientFormValue(long idClientFormValue)
        {
            return context.client_form_field_values
                .Where(p => p.IdfClientFormValue == idClientFormValue )
                                    .Select(p => new ClientFormFieldValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientFormValue = p.IdfClientFormValue,
                                        IdfFormField = p.IdfFormField,
                                        Value = p.Value,
                                    }).ToList();
        }
    }
}
