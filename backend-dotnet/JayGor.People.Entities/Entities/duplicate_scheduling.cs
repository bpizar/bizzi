using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class duplicate_scheduling
    {
        public duplicate_scheduling()
        {
            scheduling = new HashSet<scheduling>();
        }

        public long Id { get; set; }
        public string DuplicateValue { get; set; }
        public int RepeatEvery { get; set; }
        public int? Weekly_Su { get; set; }
        public int? Weekly_Mo { get; set; }
        public int? Weekly_Tu { get; set; }
        public int? Weekly_We { get; set; }
        public int? Weekly_Th { get; set; }
        public int? Weekly_Fr { get; set; }
        public int? Weekly_Sa { get; set; }
        public int? Monthly_Day { get; set; }
        public int? Yearly_Month { get; set; }
        public int? Yearly_MonthDay { get; set; }
        public int EndAfter { get; set; }
        public DateTime? EndOn { get; set; }

        public virtual ICollection<scheduling> scheduling { get; set; }
    }
}
