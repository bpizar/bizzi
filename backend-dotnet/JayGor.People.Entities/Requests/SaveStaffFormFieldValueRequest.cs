using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveStaffFormFieldValueRequest : CommonRequest
    {
        public staff_form_field_values StaffFormFieldValue { get; set; } = new staff_form_field_values();
    }
}
