using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.Entities;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {      
    
        public IEnumerable<GeoTimeTracking> GetGeoTrackingData(DateTime datex)
        {
            var result = new List<GeoTimeTracking>();
            Exception error = null;

           return context.time_tracking.ToList()
                                    .Where(c => (c.start.Date.ToString("yyyy/MM/dd") == datex.ToString("yyyy/MM/dd") || Convert.ToDateTime(c.end).ToString("yyyy/MM/dd") == datex.ToString("yyyy/MM/dd") ) )
                                    .Select(p => new GeoTimeTracking
                                    {
                                        Id = p.Id,
                                        IdfStaffProjectPosition = p.IdfStaffProjectPosition,
                                        start  = p.start,
                                        end = p.end,
                                        startNote = p.startNote,
                                        endNote = p.endNote,
                                        Longitude = p.Longitude,
                                        Latitude = p.Latitude,
                                        endLong = p.endLong,
                                        endLat = p.endLat,
                                        ElapsedTime =  Convert.ToInt64(p.end !=null ? Convert.ToDateTime(p.end).Subtract(p.start).TotalSeconds : 0),
                                        img = p.IdfStaffProjectPositionNavigation.IdfStaffNavigation.IdfUserNavigation.identity_images.Single(c => c.Id == p.IdfStaffProjectPositionNavigation.IdfStaffNavigation.IdfUserNavigation.IdfImg).Name
                                    }).ToList();
        }

		public IEnumerable<GeoTimeTrackingAuto> GetGeoTrackingAutoData(DateTime datex)
		{
			return context.time_tracking_auto.ToList()
                                    .Where(c => (c.start.Date.ToShortDateString() == datex.ToShortDateString()))
									.Select(p => new GeoTimeTrackingAuto
									{
										Id = p.Id,
                                        IdfUser = p.IdfUser,
										start = p.start,
										Longitude = p.Longitude,
										Latitude = p.Latitude,								
                                        img = p.IdfUserNavigation.identity_images.Where(c => c.Id == p.IdfUserNavigation.IdfImg).Single().Name
									}).ToList();
		}
    }
}