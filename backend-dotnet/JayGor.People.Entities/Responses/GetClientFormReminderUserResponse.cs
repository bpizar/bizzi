using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetClientFormReminderUserResponse : CommonResponse
    {
        public ClientFormReminderUsersCustomEntity ClientFormReminderUser { get; set; } = new ClientFormReminderUsersCustomEntity();
    }
}
