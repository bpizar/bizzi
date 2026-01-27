using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetClientFormValueResponse : CommonResponse
    {
        public ClientFormValuesCustomEntity ClientFormValue { get; set; } = new ClientFormValuesCustomEntity();
    }
}
