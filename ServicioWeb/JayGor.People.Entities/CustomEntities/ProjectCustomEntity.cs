using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.CustomEntities
{
    public class ProjectCustomEntity:projects
    {
        public decimal TotalHours { get; set; }

        public string abm { get; set; }

        public long? IdSPP { get; set; }
    }
}
