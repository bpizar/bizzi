using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveStaffFormFieldRequest : CommonRequest
    {
        public staff_form_fields StaffFormField { get; set; } = new staff_form_fields();
    }
}
