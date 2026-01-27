using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class client_form_field_values
    {
        public client_form_field_values()
        {
        }

        public long Id { get; set; }

        public long IdfClientFormValue { get; set; }

        public long IdfFormField { get; set; }
        
        public string Value { get; set; }
    }
}
