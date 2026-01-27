using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllProjectFormFields(out List<ProjectFormFieldsCustomEntity> projectFormFields)
        {
            projectFormFields = dataAccessLayer.GetAllProjectFormFields().ToList();
        }

        public void GetProjectFormFieldbyId(long id,
                              out ProjectFormFieldsCustomEntity projectFormFieldsOut)
        {
            projectFormFieldsOut = new ProjectFormFieldsCustomEntity();

            if (id >= 0)
            {
                projectFormFieldsOut = dataAccessLayer.GetProjectFormFieldbyId(id);
            }
        }

        public CommonResponse SaveProjectFormField(project_form_fields projectFormFields)
        {
            var result = dataAccessLayer.SaveProjectFormField(projectFormFields);
            return result;
        }

        public CommonResponse DeleteProjectFormField(long IdProjectFormField)
        {
            var result = dataAccessLayer.DeleteProjectFormFields(IdProjectFormField);
            return result;
        }

        public CommonResponse RemoveProjectFormField(long IdProjectForm, long IdFormField)
        {
            var result = dataAccessLayer.RemoveProjectFormField(IdProjectForm, IdFormField);
            return result;
        }

        public CommonResponse AddProjectFormField(long idProjectForm, string name, string description, string placeholder, string dataType, string constraints)
        {
            var result = dataAccessLayer.AddProjectFormField(idProjectForm, name, description, placeholder, dataType, constraints);
            return result;
        }
    }
}