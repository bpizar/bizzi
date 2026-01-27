using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class ReportProjectsDetailsCustomEntity
    {
        public string Period { get; set; }
        public string ProjectName { get; set; }
        public string Task { get; set; }
        public string AssignedToFullName { get; set; }
        public string DeadLine { get; set; }
        public string Hours { get; set; }
        public long Seconds { get; set; }
    }
}
