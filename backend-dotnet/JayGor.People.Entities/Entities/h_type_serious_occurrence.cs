using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_type_serious_occurrence
    {
        public h_type_serious_occurrence()
        {
            h_incidents = new HashSet<h_incidents>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string State { get; set; }

        public virtual ICollection<h_incidents> h_incidents { get; set; }
    }
}
