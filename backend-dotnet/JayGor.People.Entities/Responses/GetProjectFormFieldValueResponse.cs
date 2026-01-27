using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectFormFieldValueResponse : CommonResponse
    {
        public ProjectFormFieldValuesCustomEntity ProjectFormFieldValue { get; set; } = new ProjectFormFieldValuesCustomEntity();
    }
}
