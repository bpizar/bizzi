using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectFormImageValuesResponse : CommonResponse
    {
        public IEnumerable<ProjectFormImageValuesCustomEntity> ProjectFormImageValues { get; set; } = new List<ProjectFormImageValuesCustomEntity>();
    }
}