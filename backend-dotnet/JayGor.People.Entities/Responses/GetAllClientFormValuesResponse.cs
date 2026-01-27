using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetAllClientFormValuesResponse : CommonResponse
    {
        public IEnumerable<ClientFormValuesCustomEntity> ClientFormValues { get; set; }
    }
}
