using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.Responses
{
    public class GetPettyCashResponse : CommonResponse
    {
        public List<ProjectPettyCashCustom> PettyCash { get; set; } = new List<ProjectPettyCashCustom>();
        public List<project_pettycash_categories> PettyCashCategories { get; set; } = new List<project_pettycash_categories>();

    }
}