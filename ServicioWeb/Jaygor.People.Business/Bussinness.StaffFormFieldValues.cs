using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllStaffFormFieldValues(out List<StaffFormFieldValuesCustomEntity> staffFormFieldValues)
        {
            staffFormFieldValues = dataAccessLayer.GetAllStaffFormFieldValues().ToList();
        }

        public void GetStaffFormFieldValuebyId(long id,
                              out StaffFormFieldValuesCustomEntity staffFormFieldValuesOut)
        {
            staffFormFieldValuesOut = new StaffFormFieldValuesCustomEntity();

            if (id >= 0)
            {
                staffFormFieldValuesOut = dataAccessLayer.GetStaffFormFieldValuebyId(id);
            }
        }

        public CommonResponse SaveStaffFormFieldValue(staff_form_field_values staffFormFieldValues)
        {
            var result = dataAccessLayer.SaveStaffFormFieldValue(staffFormFieldValues);
            return result;
        }

        public CommonResponse DeleteStaffFormFieldValue(long IdStaffFormFieldValue)
        {
            var result = dataAccessLayer.DeleteStaffFormFieldValues(IdStaffFormFieldValue);
            return result;
        }

        public void GetAllStaffFormFieldValuesByStaffFormAndStaff(long idStaffForm, long idStaff, out List<StaffFormFieldValuesCustomEntity> staffFormFieldValues)
        {
            staffFormFieldValues = dataAccessLayer.GetAllStaffFormFieldValuesByStaffFormAndStaff(idStaffForm, idStaff).ToList();
        }

        public void GetAllStaffFormFieldValuesByStaffFormValue(long idStaffFormValue, out List<StaffFormFieldValuesCustomEntity> staffFormFieldValues)
        {
            staffFormFieldValues = dataAccessLayer.GetAllStaffFormFieldValuesByStaffFormValue(idStaffFormValue).ToList();
        }
    }
}