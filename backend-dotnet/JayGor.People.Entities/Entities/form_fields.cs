using System;
using System.Collections.Generic;
using System.Text;

namespace JayGor.People.Entities.Entities
{
    public class form_fields
    {
        public form_fields()
        {
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Placeholder { get; set; }

        public string Datatype { get; set; }

        public string Constraints { get; set; }
    }
}
