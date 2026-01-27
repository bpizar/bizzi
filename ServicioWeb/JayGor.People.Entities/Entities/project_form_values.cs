using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class project_form_values
    {
        public project_form_values()
        {
        }

        public long Id { get; set; }

        public long IdfProject { get; set; }
        
        public long IdfProjectForm { get; set; }

        public DateTime FormDateTime { get; set; }

        public DateTime DateTime { get; set; }
    }
}
