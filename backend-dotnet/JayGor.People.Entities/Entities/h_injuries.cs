using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_injuries
    {
        public h_injuries()
        {
            h_injury_values = new HashSet<h_injury_values>();
        }

        public long Id { get; set; }
        public long IdfPeriod { get; set; }
        public long IdfClient { get; set; }
        public long? IdfIncident { get; set; }
        public int IdfDegreeOfInjury { get; set; }
        public string DescName { get; set; }
        public string State { get; set; }
        public DateTime DateOfInjury { get; set; }
        public DateTime? DateReportedSupervisor { get; set; }
        public long? IdfSupervisor { get; set; }
        public string BodySerialized { get; set; }
        public long ProjectId { get; set; }

        public virtual clients IdfClientNavigation { get; set; }
        public virtual h_degree_of_injury IdfDegreeOfInjuryNavigation { get; set; }
        public virtual h_incidents IdfIncidentNavigation { get; set; }
        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual projects Project { get; set; }
        public virtual ICollection<h_injury_values> h_injury_values { get; set; }
    }
}
