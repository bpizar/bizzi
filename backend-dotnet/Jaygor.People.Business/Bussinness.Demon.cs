using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.Responses;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public List<tasks_reminders> GetTaskReminderByCurrentTime()
        {
            return dataAccessLayer.GetTaskReminderByCurrentTime()
                                  ;
        }

        public List<h_medical_remindersCustom> GetMedicalRemindersByCurrentTime()
        {
            return dataAccessLayer.GetMedicalRemindersByCurrentTime();
        }
    }  
}