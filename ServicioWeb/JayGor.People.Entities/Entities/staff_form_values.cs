using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class staff_form_values
    {
        public staff_form_values()
        {
        }

        public long Id { get; set; }

        public long IdfStaff { get; set; }
        
        public long IdfStaffForm { get; set; }

        public DateTime FormDateTime { get; set; }

        public DateTime DateTime { get; set; }
    }
}
