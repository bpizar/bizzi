using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class projects_clients
    {
        public long Id { get; set; }
        public long IdfProject { get; set; }
        public long IdfClient { get; set; }
        public long IdfPeriod { get; set; }
        public string State { get; set; }
        public long? IdfSPP { get; set; }

        public virtual clients IdfClientNavigation { get; set; }
        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual projects IdfProjectNavigation { get; set; }
        public virtual staff_project_position IdfSPPNavigation { get; set; }
    }
}
