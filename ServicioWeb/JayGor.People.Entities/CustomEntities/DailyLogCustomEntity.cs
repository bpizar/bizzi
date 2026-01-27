using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.CustomEntities
{
    public class DailyLogCustomEntity 
    {
        public long Id { get; set; }
        public string DateDailyLog { get; set; }        
        public string Description { get; set; }
        public string Color { get; set; }
        public string Abm { get; set; }
        public string ProjectName { get; set; }
    }
}
