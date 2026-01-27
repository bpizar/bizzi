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
        public IEnumerable<StaffFormValuesCustomEntity> GetAllStaffFormValues()
        {
            return context.staff_form_values
                                    .Select(p => new StaffFormValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfStaff = p.IdfStaff,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime,
                                    }).ToList();
        }

        public StaffFormValuesCustomEntity GetStaffFormValuebyId(long id)
        {
            return context.staff_form_values
                        .Where(c => c.Id == id)
                                    .Select(p => new StaffFormValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfStaff = p.IdfStaff,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime
                                    }).Single();
        }

        public CommonResponse SaveStaffFormValue(staff_form_values StaffFormValue)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                switch (StaffFormValue.Id)
                {
                    case (long)AbmEnum.IsNew:
                        var newStaffFormValue = new staff_form_values
                        {
                            IdfStaffForm = StaffFormValue.IdfStaffForm,
                            IdfStaff = StaffFormValue.IdfStaff,
                            FormDateTime = StaffFormValue.FormDateTime,
                            DateTime = DateTime.Now
                        };
                        context.staff_form_values.Add(newStaffFormValue);
                        context.SaveChanges();
                        break;
                    default:
                        var staff_form_valuedb = context.staff_form_values.Where(c => c.Id == StaffFormValue.Id).FirstOrDefault();
                        staff_form_valuedb.IdfStaffForm = StaffFormValue.IdfStaffForm;
                        staff_form_valuedb.IdfStaff = StaffFormValue.IdfStaff;
                        staff_form_valuedb.FormDateTime = StaffFormValue.FormDateTime;
                        staff_form_valuedb.DateTime = DateTime.Now;
                        context.staff_form_values.Update(staff_form_valuedb);
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

        public CommonResponse DeleteStaffFormValues(long staffFormValueId)
        {
            CommonResponse result = new CommonResponse(); 
            var StaffFormValueToDelete = GetStaffFormValuebyId(staffFormValueId);
            if (StaffFormValueToDelete == null)
                result.Result = false;
            context.staff_form_values.Remove(StaffFormValueToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public IEnumerable<StaffFormValuesCustomEntity> GetAllStaffFormValuesByStaffFormAndStaff(long idStaffForm, long idStaff)
        {
            return context.staff_form_values.Where(p=>p.IdfStaff==idStaff && p.IdfStaffForm==idStaffForm)
                                    .Select(p => new StaffFormValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfStaff = p.IdfStaff,
                                        DateTime = p.DateTime,
                                        FormDateTime = p.FormDateTime,
                                    }).ToList();
        }

        public CommonResponse SaveStaffFormValueWithDetail(staff_form_values StaffFormValue, staff_form_field_values[] StaffFormFieldValues)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                staff_form_values staffFormValueFromDB = null;
                if (StaffFormValue.Id>0)
                {
                    staffFormValueFromDB = context.staff_form_values.Where(p => p.Id == StaffFormValue.Id).SingleOrDefault();
                }
                if (staffFormValueFromDB==null)
                {
                    StaffFormValue.Id = 0;
                    StaffFormValue.DateTime = DateTime.Now;
                    context.staff_form_values.Add(StaffFormValue);
                    context.SaveChanges();  
                    staffFormValueFromDB = context.staff_form_values.Where(p => p.Id == StaffFormValue.Id).Single();
                }
                foreach (staff_form_field_values staff_form_field_value in StaffFormFieldValues)
                {
                    staff_form_field_value.IdfStaffFormValue = staffFormValueFromDB.Id;
                    staff_form_field_values staff_form_field_valueFromDB = context.staff_form_field_values.Where(p => p.IdfStaffFormValue == staff_form_field_value.IdfStaffFormValue && p.IdfFormField == staff_form_field_value.IdfFormField).SingleOrDefault();
                    if (staff_form_field_valueFromDB == null)
                    {
                        context.staff_form_field_values.Add(staff_form_field_value);
                        context.SaveChanges();
                    }
                    else
                    {
                        staff_form_field_valueFromDB.Value = staff_form_field_value.Value;
                        context.staff_form_field_values.Update(staff_form_field_valueFromDB);
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
        public bool UpdateStaffFormValueImage(long id, string fileName)
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
