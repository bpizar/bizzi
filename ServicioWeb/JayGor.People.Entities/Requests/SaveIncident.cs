using System;
using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;
using JayGor.People.Entities.Requests;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.Requests
{
    public class SaveIncidentRequest : CommonRequest
    {
        public h_incidents Incident { get; set; } = new h_incidents();
        public List<h_catalogCustom> Catalog { get; set; } = new List<h_catalogCustom>();
        public List<ClientCustomEntity> Clients { get; set; } = new List<ClientCustomEntity>();
        public List<h_injuries> Injuries { get; set; } = new List<h_injuries>();
        public List<h_incident_involved_people> InvolvedPeople { get; set; } = new List<h_incident_involved_people>();
    }
}



//public h_incidents Incident { get; set; }
//public List<ProjectCustomEntity> ProjectsList { get; set; } = new List<ProjectCustomEntity>();

//public List<h_degree_of_injury> DegreeOfInjuryList { get; set; } = new List<h_degree_of_injury>();
//public List<h_region> RegionList { get; set; } = new List<h_region>();
//public List<h_ministeries> Ministeries { get; set; } = new List<h_ministeries>();

//public List<h_catalogCustom> Catalog { get; set; } = new List<h_catalogCustom>();

//public List<ClientCustomEntity> Clients { get; set; } = new List<ClientCustomEntity>();
//public List<h_injuries> Injuries { get; set; } = new List<h_injuries>();

//public List<StaffCustomEntity> Staffs { get; set; } = new List<StaffCustomEntity>();
//public List<h_incident_involved_people> InvolvedPeople { get; set; } = new List<h_incident_involved_people>();
//public List<h_type_serious_occurrence> TypeSeriousOccurrence { get; set; } = new List<h_type_serious_occurrence>();

//public List<h_umab_intervention> UmabIntervention { get; set; } = new List<h_umab_intervention>();