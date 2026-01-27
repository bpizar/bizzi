using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectFormResponse : CommonResponse
    {
        public ProjectFormsCustomEntity ProjectForm { get; set; } = new ProjectFormsCustomEntity();
    }
}
