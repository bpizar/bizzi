using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_catalog
    {
        public h_catalog()
        {
            h_incident_values = new HashSet<h_incident_values>();
            h_injury_values = new HashSet<h_injury_values>();
        }

        public string id { get; set; }
        public string IdentifierGroup { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string State { get; set; }

        public virtual ICollection<h_incident_values> h_incident_values { get; set; }
        public virtual ICollection<h_injury_values> h_injury_values { get; set; }
    }
}
