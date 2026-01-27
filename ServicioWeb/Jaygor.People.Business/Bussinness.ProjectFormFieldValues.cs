using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllProjectFormFieldValues(out List<ProjectFormFieldValuesCustomEntity> projectFormFieldValues)
        {
            projectFormFieldValues = dataAccessLayer.GetAllProjectFormFieldValues().ToList();
        }

        public void GetProjectFormFieldValuebyId(long id,
                              out ProjectFormFieldValuesCustomEntity projectFormFieldValuesOut)
        {
            projectFormFieldValuesOut = new ProjectFormFieldValuesCustomEntity();

            if (id >= 0)
            {
                projectFormFieldValuesOut = dataAccessLayer.GetProjectFormFieldValuebyId(id);
            }
        }

        public CommonResponse SaveProjectFormFieldValue(project_form_field_values projectFormFieldValues)
        {
            var result = dataAccessLayer.SaveProjectFormFieldValue(projectFormFieldValues);
            return result;
        }

        public CommonResponse DeleteProjectFormFieldValue(long IdProjectFormFieldValue)
        {
            var result = dataAccessLayer.DeleteProjectFormFieldValues(IdProjectFormFieldValue);
            return result;
        }

        public void GetAllProjectFormFieldValuesByProjectFormAndProject(long idProjectForm, long idProject, out List<ProjectFormFieldValuesCustomEntity> projectFormFieldValues)
        {
            projectFormFieldValues = dataAccessLayer.GetAllProjectFormFieldValuesByProjectFormAndProject(idProjectForm, idProject).ToList();
        }

        public void GetAllProjectFormFieldValuesByProjectFormValue(long idProjectFormValue, out List<ProjectFormFieldValuesCustomEntity> projectFormFieldValues)
        {
            projectFormFieldValues = dataAccessLayer.GetAllProjectFormFieldValuesByProjectFormValue(idProjectFormValue).ToList();
        }
    }
}