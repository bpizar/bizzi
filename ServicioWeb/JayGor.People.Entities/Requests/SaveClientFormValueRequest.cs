using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveClientFormValueRequest : CommonRequest
    {
        public client_form_values ClientFormValue { get; set; } = new client_form_values();
        public client_form_field_values[] ClientFormFieldValues { get; set; } = { };
    }
}
