using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllStaffFormValues(out List<StaffFormValuesCustomEntity> staffFormValues)
        {
            staffFormValues = dataAccessLayer.GetAllStaffFormValues().ToList();
        }

        public void GetStaffFormValuebyId(long id,
                              out StaffFormValuesCustomEntity staffFormValueOut)
        {
            staffFormValueOut = new StaffFormValuesCustomEntity();

            if (id >= 0)
            {
                staffFormValueOut = dataAccessLayer.GetStaffFormValuebyId(id);
            }
        }

        public CommonResponse SaveStaffFormValue(staff_form_values staffFormValues)
        {
            var result = dataAccessLayer.SaveStaffFormValue(staffFormValues);
            return result;
        }

        public CommonResponse DeleteStaffFormValue(long IdStaffFormValue)
        {
            var result = dataAccessLayer.DeleteStaffFormValues(IdStaffFormValue);
            return result;
        }

        public void GetAllStaffFormValuesByStaffFormAndStaff(long idStaffForm, long idStaff, out List<StaffFormValuesCustomEntity> staffFormValues)
        {
            staffFormValues = dataAccessLayer.GetAllStaffFormValuesByStaffFormAndStaff(idStaffForm, idStaff).ToList();
        }

        public CommonResponse SaveStaffFormValueWithDetail(staff_form_values staffFormValues,staff_form_field_values[] StaffFormFieldValues)
        {
            var result = dataAccessLayer.SaveStaffFormValueWithDetail(staffFormValues, StaffFormFieldValues);
            return result;
        }
    }
}