using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class meetings
    {
        public long Id { get; set; }
        public long UserID { get; set; }
        public string RequiredAtt { get; set; }
        public string OptionalAtt { get; set; }
        public string Organizers { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Remind { get; set; }
        public int Active { get; set; }
        public string Type { get; set; }
    }
}
