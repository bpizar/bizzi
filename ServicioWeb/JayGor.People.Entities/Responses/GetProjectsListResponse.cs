using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectsListResponse : CommonResponse
    {
        public List<ProjectListCustomEntity> Projects { get; set; } = new List<ProjectListCustomEntity>();
    }
}
