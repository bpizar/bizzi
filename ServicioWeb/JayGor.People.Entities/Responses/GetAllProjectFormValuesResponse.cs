using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllProjectFormValuesResponse : CommonResponse
    {
        public IEnumerable<ProjectFormValuesCustomEntity> ProjectFormValues { get; set; }
    }
}
