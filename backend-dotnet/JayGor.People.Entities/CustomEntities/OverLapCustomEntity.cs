
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class OverLapCustomEntity
    {
        public long Id { get; set; }
        public string Group { get; set; }
        public string PositionName { get; set; }
		public string ProjectName { get; set; }
        public string From { get; set; }
		public string To { get; set; }
        public string Color { get; set; }
        public long IdTask { get; set; }


		// public long[] Nav { get; set; }
        public long IdNavAux { get; set; }
    }
}
