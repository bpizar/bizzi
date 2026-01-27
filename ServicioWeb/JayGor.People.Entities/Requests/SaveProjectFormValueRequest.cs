using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveProjectFormValueRequest : CommonRequest
    {
        public project_form_values ProjectFormValue { get; set; } = new project_form_values();
        public project_form_field_values[] ProjectFormFieldValues { get; set; } = { };
    }
}
