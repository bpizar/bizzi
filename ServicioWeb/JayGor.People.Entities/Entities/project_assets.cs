using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class project_assets
    {
        public long Id { get; set; }
        public float Amount { get; set; }
        public int? TaxFed { get; set; }
        public int? TaxProb { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public long ProjectId { get; set; }
        public string Category { get; set; }
    }
}
