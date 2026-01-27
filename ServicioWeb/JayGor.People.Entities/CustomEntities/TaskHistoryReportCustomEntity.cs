using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class TaskHistoryReportCustomEntity
    {
        public string Task { get; set; }
        public string Participant { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }       
    }
}
