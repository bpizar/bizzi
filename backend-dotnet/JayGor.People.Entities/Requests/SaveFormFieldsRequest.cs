using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveFormFieldRequest : CommonRequest
    {
        public form_fields FormField { get; set; } = new form_fields();
    }
}
