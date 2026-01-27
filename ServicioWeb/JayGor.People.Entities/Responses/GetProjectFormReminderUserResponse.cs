using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetProjectFormReminderUserResponse : CommonResponse
    {
        public ProjectFormReminderUsersCustomEntity ProjectFormReminderUser { get; set; } = new ProjectFormReminderUsersCustomEntity();
    }
}
