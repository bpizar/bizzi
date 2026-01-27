using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class StaffForPlanningCustomEntity
    {
        public long IdfUser { get; set; }
        public long IdfStaff { get; set; }
        public long IdfProject { get; set; }
        public string FullUserName { get; set; }        
        public string ProjectName { get; set; }
        public string PositionName { get; set; }
        public string Color { get; set; }
        public string group { get; set; }

      
        public string Img { get; set; }

		public long Hours { get; set; }
		public long HoursAssigned { get; set; }
		public long HoursFree { get; set; }

        public long MaxHoursByPeriod { get; set; }

        public string Abm { get; set; }


        public long IdfStaffProjectPosition { get; set; }
		public long IdNavAux { get; set; }
        public List<long> Nav { get; set; }
    }
}
