using System;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.CustomEntities
{
    public class ProjectClientCustomEntity : projects_clients
    {
        
        public string Color { get; set; }
        public string Description { get; set; }
        public string abm { get; set; }
        public long? IdSPP { get; set; }
    }
}
