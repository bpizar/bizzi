using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveProjectFormRequest : CommonRequest
    {
        public project_forms ProjectForm { get; set; } = new project_forms();
        
        public project_form_reminders[] ProjectFormReminders { get; set; } = { };

        public FormFieldsCustomEntity[] FormFields { get; set; } = { };
    }
}
