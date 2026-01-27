using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
	public class OverTimeCustomEntity
	{
        public long Id { get; set; }
        public string Group { get; set; }
        public string PositionName { get; set; }
        public string ProjectName { get; set; }

        public long Hours { get; set; }
        public long HoursFree { get; set; }

        public string Color { get; set; }
        public string Img { get; set; }

		public List<long> Nav { get; set; }
		public long IdfStaffProjectPosition { get; set; }
        public long IdNavAux { get; set; }

	}
}
