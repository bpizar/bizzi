using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetClientFormImageValueResponse : CommonResponse
    {
        public ClientFormImageValuesCustomEntity ClientFormImageValue { get; set; } = new ClientFormImageValuesCustomEntity();
    }
}
