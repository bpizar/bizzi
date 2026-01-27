using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class staff_form_fields
    {
        public staff_form_fields()
        {
        }

        public long Id { get; set; }

        public long IdfStaffForm { get; set; }
        
        public long IdfFormField { get; set; }

        public int Position { get; set; }

        public bool IsEnabled { get; set; }
    }
}
