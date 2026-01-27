using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveClientFormImageValueRequest : CommonRequest
    {
        public client_form_image_values ClientFormImageValue { get; set; } = new client_form_image_values();
    }
}
