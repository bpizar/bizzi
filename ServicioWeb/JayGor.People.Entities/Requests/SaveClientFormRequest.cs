using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveClientFormRequest : CommonRequest
    {
        public client_forms ClientForm { get; set; } = new client_forms();
        
        public client_form_reminders[] ClientFormReminders { get; set; } = { };

        public FormFieldsCustomEntity[] FormFields { get; set; } = { };
    }
}
