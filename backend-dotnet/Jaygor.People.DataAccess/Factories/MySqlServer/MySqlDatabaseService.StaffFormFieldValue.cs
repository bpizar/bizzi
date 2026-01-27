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
        public IEnumerable<StaffFormFieldValuesCustomEntity> GetAllStaffFormFieldValues()
        {
            return context.staff_form_field_values
                                    .Select(p => new StaffFormFieldValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffFormValue = p.IdfStaffFormValue,
                                        IdfFormField = p.IdfFormField,
                                        Value = p.Value,
                                    }).ToList();
        }

        public StaffFormFieldValuesCustomEntity GetStaffFormFieldValuebyId(long id)
        {
            return context.staff_form_field_values
                        .Where(c => c.Id == id)
                                    .Select(p => new StaffFormFieldValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffFormValue = p.IdfStaffFormValue,
                                        IdfFormField = p.IdfFormField,
                                        Value = p.Value,
                                    }).Single();
        }

        public CommonResponse SaveStaffFormFieldValue(staff_form_field_values StaffFormFieldValue)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (StaffFormFieldValue.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newStaffFormFieldValue = new staff_form_field_values
                        {
                            IdfStaffFormValue = StaffFormFieldValue.IdfStaffFormValue,
                            IdfFormField = StaffFormFieldValue.IdfFormField,
                            Value = StaffFormFieldValue.Value,
                        };
                        context.staff_form_field_values.Add(newStaffFormFieldValue);
                        context.SaveChanges();
                        break;
                    default:
                        var staff_form_field_valuedb = context.staff_form_field_values.Where(c => c.Id == StaffFormFieldValue.Id).FirstOrDefault();
                        staff_form_field_valuedb.IdfStaffFormValue = StaffFormFieldValue.IdfStaffFormValue;
                        staff_form_field_valuedb.IdfFormField = StaffFormFieldValue.IdfFormField;
                        staff_form_field_valuedb.Value = StaffFormFieldValue.Value;
                        context.staff_form_field_values.Update(staff_form_field_valuedb);
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

        public CommonResponse DeleteStaffFormFieldValues(long staffFormFieldValueId)
        {
            CommonResponse result = new CommonResponse(); 
            var StaffFormFieldValueToDelete = GetStaffFormFieldValuebyId(staffFormFieldValueId);
            if (StaffFormFieldValueToDelete == null)
                result.Result = false;
            context.staff_form_field_values.Remove(StaffFormFieldValueToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<StaffFormFieldValuesCustomEntity> GetAllStaffFormFieldValuesByStaffFormAndStaff(long idStaffForm, long idStaff)
        {
            return context.staff_form_field_values
                .Join(context.staff_form_values,
                                          sffv => sffv.IdfStaffFormValue,
                                          sfv => sfv.Id,
                                          (sffv, sfv) => new { sffv, sfv })
                .Where(p => p.sfv.IdfStaff == idStaff && p.sfv.IdfStaffForm == idStaffForm)
                                    .Select(p => new StaffFormFieldValuesCustomEntity
                                    {
                                        Id = p.sffv.Id,
                                        IdfStaffFormValue = p.sffv.IdfStaffFormValue,
                                        IdfFormField = p.sffv.IdfFormField,
                                        Value = p.sffv.Value,
                                    }).ToList();
        }

        public IEnumerable<StaffFormFieldValuesCustomEntity> GetAllStaffFormFieldValuesByStaffFormValue(long idStaffFormValue)
        {
            return context.staff_form_field_values
                .Where(p => p.IdfStaffFormValue == idStaffFormValue )
                                    .Select(p => new StaffFormFieldValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffFormValue = p.IdfStaffFormValue,
                                        IdfFormField = p.IdfFormField,
                                        Value = p.Value,
                                    }).ToList();
        }
    }
}
