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
        public IEnumerable<StaffFormFieldsCustomEntity> GetAllStaffFormFields()
        {
            return context.staff_form_fields
                                    .Select(p => new StaffFormFieldsCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfFormField= p.IdfFormField,
                                    }).ToList();
        }

        public StaffFormFieldsCustomEntity GetStaffFormFieldbyId(long id)
        {
            return context.staff_form_fields
                        .Where(c => c.Id == id)
                                    .Select(p => new StaffFormFieldsCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfFormField = p.IdfFormField,
                                    }).Single();
        }

        public CommonResponse SaveStaffFormField(staff_form_fields StaffFormField)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (StaffFormField.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newStaffFormField = new staff_form_fields
                        {
                            IdfStaffForm = StaffFormField.IdfStaffForm,
                            IdfFormField = StaffFormField.IdfFormField,
                        };
                        context.staff_form_fields.Add(newStaffFormField);
                        context.SaveChanges();
                        break;
                    default:
                        var staff_form_fielddb = context.staff_form_fields.Where(c => c.Id == StaffFormField.Id).FirstOrDefault();
                        staff_form_fielddb.IdfStaffForm = StaffFormField.IdfStaffForm;
                        staff_form_fielddb.IdfFormField = StaffFormField.IdfFormField;
                        context.staff_form_fields.Update(staff_form_fielddb);
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

        public CommonResponse DeleteStaffFormFields(long staffFormFieldId)
        {
            CommonResponse result = new CommonResponse(); 
            var StaffFormFieldToDelete = GetStaffFormFieldbyId(staffFormFieldId);
            if (StaffFormFieldToDelete == null)
                result.Result = false;
            context.staff_form_fields.Remove(StaffFormFieldToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public CommonResponse RemoveStaffFormField(long staffFormId, long FormFieldId)
        {
            CommonResponse result = new CommonResponse();

            var StaffFormFieldToDelete = context.staff_form_fields
            .Where(c => c.IdfFormField == FormFieldId && c.IdfStaffForm == staffFormId)
                        .Select(p => new StaffFormFieldsCustomEntity
                        {
                            Id = p.Id,
                            IdfStaffForm = p.IdfStaffForm,
                            IdfFormField = p.IdfFormField,
                        }).Single();
            if (StaffFormFieldToDelete == null)
                result.Result = false;
            context.staff_form_fields.Remove(StaffFormFieldToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }
        public CommonResponse AddStaffFormField(long idStaffForm, string name, string description, string placeholder, string dataType, string constraints)
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
            var staffFormFieldToAdd = new staff_form_fields { IdfStaffForm = idStaffForm , IdfFormField = formFieldToAdd.Id };
            context.staff_form_fields.Add(staffFormFieldToAdd);
            var added = context.SaveChanges();
            result.Result = added > 0;
            return result;
        }

    }
}
