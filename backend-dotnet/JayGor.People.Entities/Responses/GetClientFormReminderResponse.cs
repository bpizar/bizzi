using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetClientFormReminderResponse : CommonResponse
    {
        public ClientFormRemindersCustomEntity ClientFormReminder { get; set; } = new ClientFormRemindersCustomEntity();
    }
}
