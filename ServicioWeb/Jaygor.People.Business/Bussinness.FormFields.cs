using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllFormFields(out List<FormFieldsCustomEntity> formFields)
        {
            formFields = dataAccessLayer.GetAllFormFields().ToList();
        }

        public void GetFormFieldsbyId(long id,
                              out FormFieldsCustomEntity formFieldsOut)
        {
            formFieldsOut = new FormFieldsCustomEntity();

            if (id >= 0)
            {
                formFieldsOut = dataAccessLayer.GetFormFieldbyId(id);
            }
        }

        public CommonResponse SaveFormField(form_fields formFields)
        {
            var result = dataAccessLayer.SaveFormField(formFields);
            return result;
        }

        public CommonResponse DeleteFormField(long IdFormField)
        {
            var result = dataAccessLayer.DeleteFormField(IdFormField);
            return result;
        }

        public void GetAllFormFieldsByClientForm(long IdClientForm, out List<FormFieldsCustomEntity> clientFormFields)
        {
            clientFormFields = dataAccessLayer.GetAllFormFieldsByClientForm(IdClientForm).ToList();
        }

        public void GetAllFormFieldsByProjectForm(long IdProjectForm, out List<FormFieldsCustomEntity> projectFormFields)
        {
            projectFormFields = dataAccessLayer.GetAllFormFieldsByProjectForm(IdProjectForm).ToList();
        }

        public void GetAllFormFieldsByStaffForm(long IdStaffForm, out List<FormFieldsCustomEntity> staffFormFields)
        {
            staffFormFields = dataAccessLayer.GetAllFormFieldsByStaffForm(IdStaffForm).ToList();
        }
    }
}