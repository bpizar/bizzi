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
        public IEnumerable<ClientFormFieldsCustomEntity> GetAllClientFormFields()
        {
            return context.client_form_fields
                                    .Select(p => new ClientFormFieldsCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfFormField= p.IdfFormField,
                                    }).ToList();
        }

        public ClientFormFieldsCustomEntity GetClientFormFieldbyId(long id)
        {
            return context.client_form_fields
                        .Where(c => c.Id == id)
                                    .Select(p => new ClientFormFieldsCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfClientForm = p.IdfClientForm,
                                        IdfFormField = p.IdfFormField,
                                    }).Single();
        }

        public CommonResponse SaveClientFormField(client_form_fields ClientFormField)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (ClientFormField.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newClientFormField = new client_form_fields
                        {
                            IdfClientForm = ClientFormField.IdfClientForm,
                            IdfFormField = ClientFormField.IdfFormField,
                        };
                        context.client_form_fields.Add(newClientFormField);
                        context.SaveChanges();
                        break;
                    default:
                        var client_form_fielddb = context.client_form_fields.Where(c => c.Id == ClientFormField.Id).FirstOrDefault();
                        client_form_fielddb.IdfClientForm = ClientFormField.IdfClientForm;
                        client_form_fielddb.IdfFormField = ClientFormField.IdfFormField;
                        context.client_form_fields.Update(client_form_fielddb);
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

        public CommonResponse DeleteClientFormFields(long clientFormFieldId)
        {
            CommonResponse result = new CommonResponse(); 
            var ClientFormFieldToDelete = GetClientFormFieldbyId(clientFormFieldId);
            if (ClientFormFieldToDelete == null)
                result.Result = false;
            context.client_form_fields.Remove(ClientFormFieldToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public CommonResponse RemoveClientFormField(long clientFormId, long FormFieldId)
        {
            CommonResponse result = new CommonResponse();

            var ClientFormFieldToDelete = context.client_form_fields
            .Where(c => c.IdfFormField == FormFieldId && c.IdfClientForm == clientFormId)
                        .Select(p => new ClientFormFieldsCustomEntity
                        {
                            Id = p.Id,
                            IdfClientForm = p.IdfClientForm,
                            IdfFormField = p.IdfFormField,
                        }).Single();
            if (ClientFormFieldToDelete == null)
                result.Result = false;
            context.client_form_fields.Remove(ClientFormFieldToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }
        public CommonResponse AddClientFormField(long idClientForm, string name, string description, string placeholder, string dataType, string constraints)
        {
            CommonResponse result = new CommonResponse();
            var formFieldToAdd = context.form_fields
            .Where(c => c.Name== name && 
                    c.Description == description &&
                    c.Placeholder == placeholder &&
                    c.Datatype == dataType &&
                    c.Constraints == constraints
                    )
                        .Select(p => p).SingleOrDefault();
            if (formFieldToAdd == null)
            {
                formFieldToAdd = new form_fields
                {
                    Name = name,
                    Description = description,
                    Placeholder = placeholder,
                    Datatype = dataType,
                    Constraints = constraints
                };
                context.form_fields.Add(formFieldToAdd);
                var form_fieldsadded = context.SaveChanges();
            }
            var clientFormFieldToAdd = new client_form_fields { IdfClientForm = idClientForm , IdfFormField = formFieldToAdd.Id };
            context.client_form_fields.Add(clientFormFieldToAdd);
            var added = context.SaveChanges();
            result.Result = added > 0;
            return result;
        }

    }
}
