using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.CustomEntities
{
    public class ProjectPositionCustomEntity 
    {
        public long Id { get; set; }
        public long IdProject { get; set; }
        public string ProjectName { get; set; }
        public long IdPosition { get; set; }
        public string PositionName { get; set; }
        public string Group { get; set; }
    }
}
