using JayGor.People.Entities.Entities;
using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetClientResponse : CommonResponse
    {
        public ClientCustomEntity Client { get; set; } = new ClientCustomEntity();
        public List<h_medical_remindersCustom> Reminders { get; set; } = new List<h_medical_remindersCustom>();
        public List<StaffCustomEntity> Staff { get; set; } = new List<StaffCustomEntity>();
    }
}
