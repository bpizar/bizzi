using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllStaffFormFields(out List<StaffFormFieldsCustomEntity> staffFormFields)
        {
            staffFormFields = dataAccessLayer.GetAllStaffFormFields().ToList();
        }

        public void GetStaffFormFieldbyId(long id,
                              out StaffFormFieldsCustomEntity staffFormFieldsOut)
        {
            staffFormFieldsOut = new StaffFormFieldsCustomEntity();

            if (id >= 0)
            {
                staffFormFieldsOut = dataAccessLayer.GetStaffFormFieldbyId(id);
            }
        }

        public CommonResponse SaveStaffFormField(staff_form_fields staffFormFields)
        {
            var result = dataAccessLayer.SaveStaffFormField(staffFormFields);
            return result;
        }

        public CommonResponse DeleteStaffFormField(long IdStaffFormField)
        {
            var result = dataAccessLayer.DeleteStaffFormFields(IdStaffFormField);
            return result;
        }

        public CommonResponse RemoveStaffFormField(long IdStaffForm, long IdFormField)
        {
            var result = dataAccessLayer.RemoveStaffFormField(IdStaffForm, IdFormField);
            return result;
        }

        public CommonResponse AddStaffFormField(long idStaffForm, string name, string description, string placeholder, string dataType, string constraints)
        {
            var result = dataAccessLayer.AddStaffFormField(idStaffForm, name, description, placeholder, dataType, constraints);
            return result;
        }
    }
}