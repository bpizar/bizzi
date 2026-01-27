using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveProjectFormFieldValueRequest : CommonRequest
    {
        public project_form_field_values ProjectFormFieldValue { get; set; } = new project_form_field_values();
    }
}
