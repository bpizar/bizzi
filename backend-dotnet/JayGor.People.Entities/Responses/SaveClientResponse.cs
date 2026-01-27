using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class SaveClientResponse :CommonResponse
    {
        public List<h_medical_remindersCustom> Reminders { get; set; } = new List<h_medical_remindersCustom>();
    }
}
