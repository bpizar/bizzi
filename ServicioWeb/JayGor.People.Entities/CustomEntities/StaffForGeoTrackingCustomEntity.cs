using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class StaffForGeoTrackingCustomEntity:StaffForPlanningCustomEntity
    {
        public string CurrentState { get; set; }
    }
}
