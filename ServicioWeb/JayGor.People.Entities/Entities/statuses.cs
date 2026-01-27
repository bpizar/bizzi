using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class statuses
    {
        public statuses()
        {
            tasks = new HashSet<tasks>();
            tasks_state_history = new HashSet<tasks_state_history>();
        }

        public long Id { get; set; }
        public string status { get; set; }

        public virtual ICollection<tasks> tasks { get; set; }
        public virtual ICollection<tasks_state_history> tasks_state_history { get; set; }
    }
}
