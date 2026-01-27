using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_degree_of_injury
    {
        public h_degree_of_injury()
        {
            h_injuries = new HashSet<h_injuries>();
        }

        public int id { get; set; }
        public string Description { get; set; }
        public string State { get; set; }

        public virtual ICollection<h_injuries> h_injuries { get; set; }
    }
}
