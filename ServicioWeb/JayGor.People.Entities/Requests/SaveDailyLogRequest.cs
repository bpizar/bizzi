using System;
using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;
using JayGor.People.Entities.Requests;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.Requests
{
    public class SaveDailyLogRequest : CommonRequest
    {
        // public h_injuries Injury { get; set; }
        // public List<h_catalogCustom> Catalog { get; set; }
        // public List<List<PointBody>> Points { get; set; }

        public h_dailylogs DailyLog { get; set; }
        public List<h_dailylog_involved_people> InvolvedPeople { get; set; } = new List<h_dailylog_involved_people>();
        //public List<StaffCustomEntity> Staffs { get; set; } = new List<StaffCustomEntity>();

        //public string ClientName { get; set; }
        // public string ClientImg { get; set; }
        public int TimeDifference { get; set; }

    }
}