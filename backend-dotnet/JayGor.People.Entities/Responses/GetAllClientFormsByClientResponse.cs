using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllClientFormsbyClientResponse : CommonResponse
    {
        public IEnumerable<ClientFormbyClientCustomEntity> ClientFormsbyClient { get; set; }
    }
}
