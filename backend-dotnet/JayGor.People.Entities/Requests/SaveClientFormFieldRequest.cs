using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveClientFormFieldRequest : CommonRequest
    {
        public client_form_fields ClientFormField { get; set; } = new client_form_fields();
    }
}
