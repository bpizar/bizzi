using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class staff_form_field_values
    {
        public staff_form_field_values()
        {
        }

        public long Id { get; set; }

        public long IdfStaffFormValue { get; set; }
        public long IdfFormField { get; set; }
        
        public string Value { get; set; }
    }
}
