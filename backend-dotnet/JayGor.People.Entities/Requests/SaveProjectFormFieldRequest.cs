using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveProjectFormFieldRequest : CommonRequest
    {
        public project_form_fields ProjectFormField { get; set; } = new project_form_fields();
    }
}
