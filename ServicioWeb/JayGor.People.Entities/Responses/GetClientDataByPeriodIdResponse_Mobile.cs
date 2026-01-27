using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.Responses
{
    public class GetClientDataByPeriodIdResponse_Mobile : CommonResponse
    {
       public List<DailyLogCustomEntity> DailyLogsList { get; set; }

        //public List<IncidentCustomEntity> IncidentsList { get; set; }
        //  aqui el otro

        public List<InjuriesCustom> InjuriesList { get; set; }



        
        //public List<ProjectCustomEntity> ProjectClient { get; set; }
        public List<ProjectClientCustomEntity> ProjectClient { get; set; }

        public List<StaffCustomEntity> StaffList { get; set; }
    }
}