using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class reminders
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateRemind { get; set; }
        public string Type { get; set; }
        public int Active { get; set; }
    }
}
