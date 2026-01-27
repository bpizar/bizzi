using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectFormImageValueResponse : CommonResponse
    {
        public ProjectFormImageValuesCustomEntity ProjectFormImageValue { get; set; } = new ProjectFormImageValuesCustomEntity();
    }
}
