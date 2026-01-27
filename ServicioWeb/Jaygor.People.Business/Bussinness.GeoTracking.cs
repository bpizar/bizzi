
using System;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;


namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {

        public void GetGeoTacking(long idperiod, DateTime datex,bool getAuto, out List<StaffForGeoTrackingCustomEntity> staff, out List<GeoTimeTracking> geoTimeTracking, out List<GeoTimeTrackingAuto> geoTimeTrackingAuto)
        {
            staff = new List<StaffForGeoTrackingCustomEntity>();
            geoTimeTracking = new List<GeoTimeTracking>();
            geoTimeTrackingAuto = new List<GeoTimeTrackingAuto>();

            staff = dataAccessLayer.GetStaffForGeoTracking(idperiod).ToList();
			geoTimeTracking = dataAccessLayer.GetGeoTrackingData(datex).ToList();


           // datex = new DateTime(datex.Year, datex.Month, datex.Day, 0, 0, 0);


            foreach (var s in staff)
            {
                s.Hours = Convert.ToInt32(geoTimeTracking.Where(c => c.IdfStaffProjectPosition == s.IdfStaffProjectPosition && c.end != null && c.start != null).Sum(x => Convert.ToDateTime(x.end).Subtract(x.start).TotalSeconds));
                //s.Hours += Convert.ToDecimal(geoTimeTracking.Where(c => c.IdfStaffProjectPosition == s.IdfStaffProjectPosition && c.End == null && c.Start != null).Sum(x =>DateTime.Now.Subtract(x.Start).TotalSeconds));

                var lastGeoRecord = geoTimeTracking.Where(c => c.IdfStaffProjectPosition == s.IdfStaffProjectPosition).OrderBy(c => c.start).LastOrDefault();
                s.CurrentState = lastGeoRecord == null ? "none" : lastGeoRecord.end != null ? "ckeckout" : "checkin";
            }

            if (getAuto)
            {
                geoTimeTrackingAuto = dataAccessLayer.GetGeoTrackingAutoData(datex).ToList();
            }

            // laburar staffs
        }


    }
}