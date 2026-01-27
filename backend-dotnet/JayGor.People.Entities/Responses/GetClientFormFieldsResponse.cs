using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetClientFormFieldsResponse : CommonResponse
    {
        public IEnumerable<ClientFormFieldsCustomEntity> ClientFormFields { get; set; } = new List<ClientFormFieldsCustomEntity>();
        
    }
}