using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllProjectFormImageValues(out List<ProjectFormImageValuesCustomEntity> projectFormImageValues)
        {
            projectFormImageValues = dataAccessLayer.GetAllProjectFormImageValues().ToList();
        }

        public void GetProjectFormImageValuebyId(long id,
                              out ProjectFormImageValuesCustomEntity projectFormImageValueOut)
        {
            projectFormImageValueOut = new ProjectFormImageValuesCustomEntity();

            if (id >= 0)
            {
                projectFormImageValueOut = dataAccessLayer.GetProjectFormImageValuebyId(id);
            }
        }

        public async Task<CommonResponse> SaveProjectFormImageValue(project_form_image_values projectFormImageValues)
        {
            return dataAccessLayer.SaveProjectFormImageValue(projectFormImageValues);
        }

        public CommonResponse DeleteProjectFormImageValue(long IdProjectFormImageValue)
        {
            var result = dataAccessLayer.DeleteProjectFormImageValues(IdProjectFormImageValue);
            return result;
        }

        public void GetProjectFormImageValueByProjectFormAndProject(long idProjectForm, long idProject, out ProjectFormImageValuesCustomEntity projectFormImageValue)
        {
            projectFormImageValue = dataAccessLayer.GetProjectFormImageValueByProjectFormAndProject(idProjectForm, idProject);
        }
    }
}