using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.Responses
{
    public class GetInjuryByIdResponse : CommonResponse
    {
        public h_injuries Injury { get; set; }
        public List<h_degree_of_injury> DegreeOfInjuryList { get; set; } = new List<h_degree_of_injury>();
        public List<StaffCustomEntity> Staffs { get; set; } = new List<StaffCustomEntity>();


        public List<h_catalogCustom> Catalog { get; set; } = new List<h_catalogCustom>();


        public string ClientName { get; set; }
        public string ClientImg { get; set; }

        public List<List<PointBody>> Points {get;set;}

        
        public List<ProjectCustomEntity> Projects { get; set; } = new List<ProjectCustomEntity>();

        // public List<ProjectClientCustomEntity> Projects { get; set; } = new List<ProjectClientCustomEntity>();
    }
}










//public List<ProjectCustomEntity> ProjectsList { get; set; } = new List<ProjectCustomEntity>();


// public List<h_region> RegionList { get; set; } = new List<h_region>();
// public List<h_ministeries> Ministeries { get; set; } = new List<h_ministeries>();

// public List<h_catalogCustom> Catalog { get; set; } = new List<h_catalogCustom>();

// public List<ClientCustomEntity> Clients { get; set; } = new List<ClientCustomEntity>();
// public List<h_injuries> Injuries { get; set; } = new List<h_injuries>();


// public List<h_incident_involved_people> InvolvedPeople { get; set; } = new List<h_incident_involved_people>();
// public List<h_type_serious_occurrence> TypeSeriousOccurrence { get; set; } = new List<h_type_serious_occurrence>();