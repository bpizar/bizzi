using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class scheduling
    {
        public long Id { get; set; }
        public string State { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public long? IdfAssignedTo { get; set; }
        public int? AllDay { get; set; }
        public long? IdfCreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public long? IdDuplicate { get; set; }
        public long? IdfProject { get; set; }
        public long? IdfPeriod { get; set; }

        public virtual duplicate_scheduling IdDuplicateNavigation { get; set; }
        public virtual staff_project_position IdfAssignedToNavigation { get; set; }
        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual projects IdfProjectNavigation { get; set; }
    }
}
