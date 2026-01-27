using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class positions
    {
        public positions()
        {
            staff_project_position = new HashSet<staff_project_position>();
            tasks = new HashSet<tasks>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }

        public virtual ICollection<staff_project_position> staff_project_position { get; set; }
        public virtual ICollection<tasks> tasks { get; set; }
    }
}
