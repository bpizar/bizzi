

using System.Collections.Generic;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.Responses
{
	public class GetPettyCashCategoriesResponse : CommonResponse
	{
        public List<project_pettycash_categories> Categories { get; set; } = new List<project_pettycash_categories>();
	}
}