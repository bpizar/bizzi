using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_incident_involved_people
    {
        public long id { get; set; }
        public long idfIncident { get; set; }
        public long IdfSPP { get; set; }
        public string IdentifierGroup { get; set; }
        public string State { get; set; }

        public virtual staff_project_position IdfSPPNavigation { get; set; }
        public virtual h_incidents idfIncidentNavigation { get; set; }
    }
}
