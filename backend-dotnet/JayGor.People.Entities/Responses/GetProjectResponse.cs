using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectResponse : CommonResponse
    {
        public ProjectCustomEntity Project { get; set; } = new ProjectCustomEntity();
        public List<StaffCustomEntity> Staffs { get; set; } = new List<StaffCustomEntity>();
        public List<positions> Positions { get; set; } = new List<positions>();
        public List<settings_reminder_time> SettingsReminderTime { get; set; } = new List<settings_reminder_time>();
        public List<ClientCustomEntity> clientsAllPeriods  {get;set;} = new List<ClientCustomEntity>();
    }
}