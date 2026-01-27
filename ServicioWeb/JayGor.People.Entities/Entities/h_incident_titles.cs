using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_incident_titles
    {
        public h_incident_titles()
        {
            h_incident_catalog = new HashSet<h_catalog>();
            h_incident_involved_people = new HashSet<h_incident_involved_people>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<h_catalog> h_incident_catalog { get; set; }
        public ICollection<h_incident_involved_people> h_incident_involved_people { get; set; }
    }
}
