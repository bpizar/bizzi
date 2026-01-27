using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class project_forms
    {
        public project_forms()
        {
        }

        public long Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Information { get; set; }
        
        public string Template { get; set; }
        
        public string TemplateFile { get; set; }

        public long IdfRecurrence { get; set; }

        public long IdfRecurrenceDetail { get; set; }
    }
}
