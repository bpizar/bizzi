using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class time_tracking_auto
    {
        public long Id { get; set; }
        public long IdfUser { get; set; }
        public DateTime start { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }

        public virtual identity_users IdfUserNavigation { get; set; }
    }
}
