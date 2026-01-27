using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllProjectFormsbyProjectResponse : CommonResponse
    {
        public IEnumerable<ProjectFormbyProjectCustomEntity> ProjectFormsbyProject { get; set; }
    }
}
