using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllProjectFormValues(out List<ProjectFormValuesCustomEntity> projectFormValues)
        {
            projectFormValues = dataAccessLayer.GetAllProjectFormValues().ToList();
        }

        public void GetProjectFormValuebyId(long id,
                              out ProjectFormValuesCustomEntity projectFormValueOut)
        {
            projectFormValueOut = new ProjectFormValuesCustomEntity();

            if (id >= 0)
            {
                projectFormValueOut = dataAccessLayer.GetProjectFormValuebyId(id);
            }
        }

        public CommonResponse SaveProjectFormValue(project_form_values projectFormValues)
        {
            var result = dataAccessLayer.SaveProjectFormValue(projectFormValues);
            return result;
        }

        public CommonResponse DeleteProjectFormValue(long IdProjectFormValue)
        {
            var result = dataAccessLayer.DeleteProjectFormValues(IdProjectFormValue);
            return result;
        }

        public void GetAllProjectFormValuesByProjectFormAndProject(long idProjectForm, long idProject, out List<ProjectFormValuesCustomEntity> projectFormValues)
        {
            projectFormValues = dataAccessLayer.GetAllProjectFormValuesByProjectFormAndProject(idProjectForm, idProject).ToList();
        }

        public CommonResponse SaveProjectFormValueWithDetail(project_form_values projectFormValues,project_form_field_values[] ProjectFormFieldValues)
        {
            var result = dataAccessLayer.SaveProjectFormValueWithDetail(projectFormValues, ProjectFormFieldValues);
            return result;
        }
    }
}