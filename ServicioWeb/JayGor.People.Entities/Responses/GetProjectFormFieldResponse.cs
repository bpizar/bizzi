using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectFormFieldResponse : CommonResponse
    {
        public ProjectFormFieldsCustomEntity ProjectFormField { get; set; } = new ProjectFormFieldsCustomEntity();
    }
}
