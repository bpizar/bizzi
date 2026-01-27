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
        public IEnumerable<StaffFormImageValuesCustomEntity> GetAllStaffFormImageValues()
        {
            return context.staff_form_image_values
                                    .Select(p => new StaffFormImageValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfStaff = p.IdfStaff,
                                        Image = p.Image,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime,
                                    }).ToList();
        }

        public StaffFormImageValuesCustomEntity GetStaffFormImageValuebyId(long id)
        {
            return context.staff_form_image_values
                        .Where(c => c.Id == id)
                                    .Select(p => new StaffFormImageValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfStaff = p.IdfStaff,
                                        Image = p.Image,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime
                                    }).Single();
        }

        public CommonResponse SaveStaffFormImageValue(staff_form_image_values StaffFormImageValue)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();
            try
            {
                staff_form_image_values staffFormImageValueFromDB = null;
                if (StaffFormImageValue.Id > 0)
                {
                    staffFormImageValueFromDB = context.staff_form_image_values.Where(p => p.Id == StaffFormImageValue.Id).SingleOrDefault();
                }
                if (staffFormImageValueFromDB is null)
                {
                    StaffFormImageValue.Id = 0;
                    StaffFormImageValue.DateTime = DateTime.Now;
                    context.staff_form_image_values.Add(StaffFormImageValue);
                    context.SaveChanges();
                    staffFormImageValueFromDB = context.staff_form_image_values.Where(p => p.Id == StaffFormImageValue.Id).Single();
                }
                else
                {
                    staffFormImageValueFromDB.Image = StaffFormImageValue.Image?? staffFormImageValueFromDB.Image;
                    staffFormImageValueFromDB.DateTime = DateTime.Now;
                    staffFormImageValueFromDB.FormDateTime = StaffFormImageValue.FormDateTime;
                    context.staff_form_image_values.Update(staffFormImageValueFromDB);
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

        public CommonResponse DeleteStaffFormImageValues(long staffFormImageValueId)
        {
            CommonResponse result = new CommonResponse(); 
            var StaffFormImageValueToDelete = GetStaffFormImageValuebyId(staffFormImageValueId);
            if (StaffFormImageValueToDelete == null)
                result.Result = false;
            context.staff_form_image_values.Remove(StaffFormImageValueToDelete);
            var deleted = context.SaveChanges();
            result.Result = deleted > 0;
            return result;
        }

        public StaffFormImageValuesCustomEntity GetStaffFormImageValueByStaffFormAndStaff(long idStaffForm, long idStaff)
        {
            return context.staff_form_image_values.Where(p=>p.IdfStaff==idStaff && p.IdfStaffForm==idStaffForm)
                                    .Select(p => new StaffFormImageValuesCustomEntity
                                    {
                                        Id = p.Id,
                                        IdfStaffForm = p.IdfStaffForm,
                                        IdfStaff = p.IdfStaff,
                                        Image = p.Image,
                                        FormDateTime = p.FormDateTime,
                                        DateTime = p.DateTime
                                    }).FirstOrDefault();
        }   
    }
}
