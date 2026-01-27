using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public IEnumerable<PeriodsCustom> GetPeriods()
        {
            return dataAccessLayer.GetPeriods()
                                  .OrderByDescending(c => c.From)
                                  .OrderByDescending(c => c.To)
                                  .OrderByDescending(c => c.Id);


        }

        public CommonResponse SavePeriods(List<PeriodsCustom> periods, int workingHoursDefaultByPeriodStaff)
        {
            foreach (var f in periods.Where(c=>c.Abm=="I"))
            {
                f.From = new System.DateTime(f.From.Year, f.From.Month, f.From.Day,0, 0, 1);
                f.To = new System.DateTime(f.To.Year, f.To.Month, f.To.Day, 23, 59, 59);
            }

            return dataAccessLayer.SavePeriods(periods, workingHoursDefaultByPeriodStaff);
        }

        public GenericPair GetLastActivePeriodAndDesc()
        {
            return dataAccessLayer.GetLastActivePeriodAndDesc();
        }

    }
}