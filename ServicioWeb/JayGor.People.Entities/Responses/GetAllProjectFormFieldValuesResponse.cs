using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllProjectFormFieldValuesResponse : CommonResponse
    {
        public IEnumerable<ProjectFormFieldValuesCustomEntity> ProjectFormFieldValues { get; set; }
    }
}
