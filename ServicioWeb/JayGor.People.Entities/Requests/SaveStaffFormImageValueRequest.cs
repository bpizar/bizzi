using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveStaffFormImageValueRequest : CommonRequest
    {
        public staff_form_image_values StaffFormImageValue { get; set; } = new staff_form_image_values();
    }
}
