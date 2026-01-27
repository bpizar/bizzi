using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_incidents
    {
        public h_incidents()
        {
            h_clients_incident = new HashSet<h_clients_incident>();
            h_incident_involved_people = new HashSet<h_incident_involved_people>();
            h_incident_values = new HashSet<h_incident_values>();
            h_injuries = new HashSet<h_injuries>();
        }

        public long id { get; set; }
        public long IdfPeriod { get; set; }
        public DateTime DateIncident { get; set; }
        public TimeSpan? TimeIncident { get; set; }
        public int IsSeriousOcurrence { get; set; }
        public int IdfTypeOfSeriousOccurrence { get; set; }
        public int? IdfRegion { get; set; }
        public DateTime? DateTimeWhenSeriousOccurrence { get; set; }
        public int SentToMinistry { get; set; }
        public int? IdfMinistry { get; set; }
        public string State { get; set; }
        public string DescName { get; set; }
        public int IdfUmabIntervention { get; set; }

        public virtual h_ministeries IdfMinistryNavigation { get; set; }
        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual h_region IdfRegionNavigation { get; set; }
        public virtual h_type_serious_occurrence IdfTypeOfSeriousOccurrenceNavigation { get; set; }
        public virtual ICollection<h_clients_incident> h_clients_incident { get; set; }
        public virtual ICollection<h_incident_involved_people> h_incident_involved_people { get; set; }
        public virtual ICollection<h_incident_values> h_incident_values { get; set; }
        public virtual ICollection<h_injuries> h_injuries { get; set; }
    }
}
