using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;
using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class SaveClientRequest : CommonRequest
    {
        public ClientCustomEntity Client { get; set; } = new ClientCustomEntity();
        public List<h_medical_remindersCustom> Reminders { get; set; } = new List<h_medical_remindersCustom>();
        public List<ProjectClientCustomEntity> ProjectClient { get; set; } = new List<ProjectClientCustomEntity>();
    }
}