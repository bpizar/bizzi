using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.CustomEntities
{
    public class ProjectOwnersCustom : project_owners
    {
        public string FullName { get; set; }

        public long IdfStaff { get; set; }

        public string Abm { get; set; }
    }
}
