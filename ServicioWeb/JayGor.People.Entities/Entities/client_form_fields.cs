using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class client_form_fields
    {
        public client_form_fields()
        {
        }

        public long Id { get; set; }

        public long IdfClientForm { get; set; }
        
        public long IdfFormField { get; set; }
        
        public int Position { get; set; }

        public bool IsEnabled { get; set; }
    }
}
