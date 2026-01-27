using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllClientFormImageValuesResponse : CommonResponse
    {
        public IEnumerable<ClientFormImageValuesCustomEntity> ClientFormImageValues { get; set; }
    }
}
