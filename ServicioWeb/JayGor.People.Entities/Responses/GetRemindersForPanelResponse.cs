using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class GetRemindersForPanelResponse: CommonResponse
    {
        public List<ReminderForPanel> ReminderToday { get; set; } = new List<ReminderForPanel>();
        public List<ReminderForPanel> RemindersTomorrow { get; set; } = new List<ReminderForPanel>();
        public List<ReminderForPanel> RemindersOthers { get; set; } = new List<ReminderForPanel>();
        public List<ReminderForPanel> RemindersMedicals { get; set; } = new List<ReminderForPanel>();
    }
}
