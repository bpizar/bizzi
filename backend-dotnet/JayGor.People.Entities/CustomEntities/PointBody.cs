using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    [Serializable]
    public class PointBody 
    {     
        public double x { get; set; }
        public double y{ get; set; }
        public long time { get; set; }
        public string color { get; set; }
    }
}