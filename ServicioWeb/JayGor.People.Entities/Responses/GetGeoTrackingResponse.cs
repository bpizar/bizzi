using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetGeoTrackingResponse : CommonResponse
    {
        public List<StaffForGeoTrackingCustomEntity> StaffForGeoTrackingList { get; set; } = new List<StaffForGeoTrackingCustomEntity>();
        public List<GeoTimeTracking> GeoTimeTrackingList { get; set; } = new List<GeoTimeTracking>();
        public List<GeoTimeTrackingAuto> GeoTimeTrackingAuto { get; set; } = new List<GeoTimeTrackingAuto>();
    }
}