using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetClientFormResponse : CommonResponse
    {
        public ClientFormsCustomEntity ClientForm { get; set; } = new ClientFormsCustomEntity();
    }
}
