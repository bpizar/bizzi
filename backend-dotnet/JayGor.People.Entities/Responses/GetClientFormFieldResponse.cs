using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetClientFormFieldResponse : CommonResponse
    {
        public ClientFormFieldsCustomEntity ClientFormField { get; set; } = new ClientFormFieldsCustomEntity();
    }
}
