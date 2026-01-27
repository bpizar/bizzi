using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveStaffFormValueRequest : CommonRequest
    {
        public staff_form_values StaffFormValue { get; set; } = new staff_form_values();
        public staff_form_field_values[] StaffFormFieldValues { get; set; } = { };
    }
}
