using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class todo_items
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public sbyte Remind { get; set; }
    }
}
