using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class tasks_state_history
    {
        public long Id { get; set; }
        public long IdfTask { get; set; }
        public long IdfUser { get; set; }
        public DateTime CurrentDate { get; set; }
        public long IdfState { get; set; }

        public virtual statuses IdfStateNavigation { get; set; }
        public virtual tasks IdfTaskNavigation { get; set; }
        public virtual identity_users IdfUserNavigation { get; set; }
    }
}
