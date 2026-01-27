using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class project_form_fields
    {
        public project_form_fields()
        {
        }

        public long Id { get; set; }

        public long IdfProjectForm { get; set; }
        
        public long IdfFormField { get; set; }
     
        public int Position { get; set; }

        public bool IsEnabled { get; set; }
    }
}
