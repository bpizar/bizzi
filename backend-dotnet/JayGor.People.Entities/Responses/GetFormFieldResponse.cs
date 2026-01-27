using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetFormFieldResponse : CommonResponse
    {
        public FormFieldsCustomEntity FormField { get; set; } = new FormFieldsCustomEntity();
    }
}
