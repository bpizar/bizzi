using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.Entities;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public statuses GetStatus(long id)
        {
           return context.statuses.Where(c => c.Id == id).Single();                
        }

        public IEnumerable<statuses> GetStatuses()
        {
           return context.statuses.ToList();                
        }
    }
}