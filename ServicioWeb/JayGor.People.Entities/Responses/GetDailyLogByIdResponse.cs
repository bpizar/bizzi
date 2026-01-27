using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.Responses
{
    public class GetDailyLogByIdResponse : CommonResponse
    {
        public h_dailylogs DailyLog { get; set; }
        public List<h_dailylog_involved_people> InvolvedPeople { get; set; } = new List<h_dailylog_involved_people>();
        public List<StaffCustomEntity> Staffs { get; set; } = new List<StaffCustomEntity>();

        public string ClientName { get; set; }
        public string ClientImg { get; set; }



        // ProjectClientCustomEntity
        public List<ProjectCustomEntity> Projects { get; set; } = new List<ProjectCustomEntity>();
        //public List<ProjectClientCustomEntity> Projects { get; set; } = new List<ProjectClientCustomEntity>();

    }
}
