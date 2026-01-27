using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetClientFormImageValuesResponse : CommonResponse
    {
        public IEnumerable<ClientFormImageValuesCustomEntity> ClientFormImageValues { get; set; } = new List<ClientFormImageValuesCustomEntity>();
    }
}