using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.CustomEntities
{
    public class PeriodsCustom : periods
    {
        public string DateJoin { get; set; }
        public string Abm { get; set; }        
    }
}
