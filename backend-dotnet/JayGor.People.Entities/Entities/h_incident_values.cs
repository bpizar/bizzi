using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_incident_values
    {
        public long id { get; set; }
        public long idfIncident { get; set; }
        public string idfCatalog { get; set; }
        public string Value { get; set; }

        public virtual h_catalog idfCatalogNavigation { get; set; }
        public virtual h_incidents idfIncidentNavigation { get; set; }
    }
}
