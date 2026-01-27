using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetFormFieldsResponse : CommonResponse
    {
        public IEnumerable<FormFieldsCustomEntity> StaffForms { get; set; } = new List<FormFieldsCustomEntity>();
        
    }
}