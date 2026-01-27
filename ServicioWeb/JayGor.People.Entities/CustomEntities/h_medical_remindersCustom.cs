using System;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.CustomEntities
{
    public class h_medical_remindersCustom: h_medical_reminders    {
      
        public string abm { get; set; }
        public string ProjectName { get; set; }
        public string Color { get; set; }
        public string SppDescription { get; set; }
        public string Client { get; set; }
        public long IdUser { get; set; }

        public string Img { get; set; }


    }
}
