using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveProjectFormImageValueRequest : CommonRequest
    {
        public project_form_image_values ProjectFormImageValue { get; set; } = new project_form_image_values();
    }
}
