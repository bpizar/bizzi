using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetClientFormFieldValueResponse : CommonResponse
    {
        public ClientFormFieldValuesCustomEntity ClientFormFieldValue { get; set; } = new ClientFormFieldValuesCustomEntity();
    }
}
