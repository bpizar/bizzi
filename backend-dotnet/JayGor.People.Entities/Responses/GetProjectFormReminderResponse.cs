using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectFormReminderResponse : CommonResponse
    {
        public ProjectFormRemindersCustomEntity ProjectFormReminder { get; set; } = new ProjectFormRemindersCustomEntity();
    }
}
