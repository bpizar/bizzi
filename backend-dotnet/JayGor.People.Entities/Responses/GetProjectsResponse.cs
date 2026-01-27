using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectsResponse : CommonResponse
    {
        public List<ProjectCustomEntity> Projects { get; set; } = new List<ProjectCustomEntity>();
    }
}
