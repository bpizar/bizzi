using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectFormValueResponse : CommonResponse
    {
        public ProjectFormValuesCustomEntity ProjectFormValue { get; set; } = new ProjectFormValuesCustomEntity();
    }
}
