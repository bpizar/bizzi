using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class project_owners
    {
        public long Id { get; set; }
        public long IdfProject { get; set; }
        public long IdfOwner { get; set; }
        public string State { get; set; }
        public long IdfPeriod { get; set; }

        public virtual staff IdfOwnerNavigation { get; set; }
        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual projects IdfProjectNavigation { get; set; }
    }
}
