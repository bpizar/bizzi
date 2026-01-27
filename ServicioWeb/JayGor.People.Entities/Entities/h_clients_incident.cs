using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_clients_incident
    {
        public long Id { get; set; }
        public long IdfClient { get; set; }
        public long IdfIncident { get; set; }
        public string State { get; set; }

        public virtual clients IdfClientNavigation { get; set; }
        public virtual h_incidents IdfIncidentNavigation { get; set; }
    }
}
