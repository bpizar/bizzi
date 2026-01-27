using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllProjectForms(out List<ProjectFormsCustomEntity> projectForms)
        {
            projectForms = dataAccessLayer.GetAllProjectForms().ToList();
        }

        public void GetProjectFormbyId(long id,
                              out ProjectFormsCustomEntity projectFormsOut)
        {
            projectFormsOut = new ProjectFormsCustomEntity();

            if (id >= 0)
            {
                projectFormsOut = dataAccessLayer.GetProjectFormbyId(id);
            }
        }

        public CommonResponse SaveProjectForm(project_forms projectForms)
        {
            var result = dataAccessLayer.SaveProjectForm(projectForms);
            return result;
        }

        public CommonResponse DeleteProjectForm(long IdProjectForm)
        {
            var result = dataAccessLayer.DeleteProjectForms(IdProjectForm);
            return result;
        }

        public void GetAllProjectFormByProjects(long IdProject, out List<ProjectFormbyProjectCustomEntity> projectFormbyProjectCustomEntity)
        {
            projectFormbyProjectCustomEntity = dataAccessLayer.GetAllProjectFormsByProject(IdProject).ToList();
        }

        public void GetAllProjectFormsByProjectandProjectForm(long IdProject, long IdProjectForm, out List<ProjectFormbyProjectCustomEntity> projectFormbyProjectCustomEntity)
        {
            projectFormbyProjectCustomEntity = dataAccessLayer.GetAllProjectFormsByProjectandProjectForm(IdProject, IdProjectForm).ToList();
        }

        public CommonResponse SaveProjectFormWithReminders(project_forms projectForm, project_form_reminders[] ProjectFormReminders, FormFieldsCustomEntity[] FormFields)
        {
            var result = dataAccessLayer.SaveProjectFormWithReminders(projectForm, ProjectFormReminders, FormFields);
            return result;
        }
    }
}