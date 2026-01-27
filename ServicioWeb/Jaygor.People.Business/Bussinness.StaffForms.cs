using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllStaffForms(out List<StaffFormsCustomEntity> staffForms)
        {
            staffForms = dataAccessLayer.GetAllStaffForms().ToList();
        }

        public void GetStaffFormbyId(long id,
                              out StaffFormsCustomEntity staffFormsOut)
        {
            staffFormsOut = new StaffFormsCustomEntity();

            if (id >= 0)
            {
                staffFormsOut = dataAccessLayer.GetStaffFormbyId(id);
            }
        }

        public CommonResponse SaveStaffForm(staff_forms staffForms)
        {
            var result = dataAccessLayer.SaveStaffForm(staffForms);
            return result;
        }

        public CommonResponse DeleteStaffForm(long IdStaffForm)
        {
            var result = dataAccessLayer.DeleteStaffForms(IdStaffForm);
            return result;
        }

        public void GetAllStaffFormsByStaff(long IdStaff, out List<StaffFormbyStaffCustomEntity> staffFormbyStaffCustomEntity)
        {
            staffFormbyStaffCustomEntity = dataAccessLayer.GetAllStaffFormsByStaff(IdStaff).ToList();
        }

        public void GetAllStaffFormsByStaffandStaffForm(long IdStaff, long IdStaffForm, out List<StaffFormbyStaffCustomEntity> staffFormbyStaffCustomEntity)
        {
            staffFormbyStaffCustomEntity = dataAccessLayer.GetAllStaffFormsByStaffandStaffForm(IdStaff, IdStaffForm).ToList();
        }

        public CommonResponse SaveStaffFormWithReminders(staff_forms staffForm, staff_form_reminders[] StaffFormReminders, FormFieldsCustomEntity[] FormFields)
        {
            var result = dataAccessLayer.SaveStaffFormWithReminders(staffForm, StaffFormReminders, FormFields);
            return result;
        }
    }
}