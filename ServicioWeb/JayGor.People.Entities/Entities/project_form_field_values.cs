using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class project_form_field_values
    {
        public project_form_field_values()
        {
        }

        public long Id { get; set; }

        public long IdfProjectFormValue { get; set; }

        public long IdfFormField { get; set; }
        
        public string Value { get; set; }
    }
}
