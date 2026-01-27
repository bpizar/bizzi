using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveStaffFormRequest : CommonRequest
    {
        public staff_forms StaffForm { get; set; } = new staff_forms();

        public staff_form_reminders[] StaffFormReminders { get; set; } = { };

        public FormFieldsCustomEntity[] FormFields { get; set; } = { };
    }
}
