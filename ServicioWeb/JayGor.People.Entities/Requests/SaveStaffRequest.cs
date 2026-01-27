using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveStaffRequest : CommonRequest
    {
        public staff Staff { get; set; } = new staff();
        public identity_users User { get; set; } = new identity_users();
        public List<long> Roles { get; set; } = new List<long>();
        public staff_period_settings staffPeriodSettings { get; set; } = new staff_period_settings(); 
        public long IdfPeriod { get; set; }


      


    }
}
