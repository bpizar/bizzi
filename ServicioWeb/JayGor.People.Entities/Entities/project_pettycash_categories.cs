using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class project_pettycash_categories
    {
        public project_pettycash_categories()
        {
            project_petty_cash = new HashSet<project_petty_cash>();
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public string State { get; set; }

        public virtual ICollection<project_petty_cash> project_petty_cash { get; set; }
    }
}
