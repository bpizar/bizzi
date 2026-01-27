using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveClientFormFieldValueRequest : CommonRequest
    {
        public client_form_field_values ClientFormFieldValue { get; set; } = new client_form_field_values();
    }
}
