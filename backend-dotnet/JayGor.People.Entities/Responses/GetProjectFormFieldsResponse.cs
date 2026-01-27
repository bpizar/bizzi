using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectFormFieldsResponse : CommonResponse
    {
        public IEnumerable<ProjectFormFieldsCustomEntity> ProjectFormFields { get; set; } = new List<ProjectFormFieldsCustomEntity>();
        
    }
}