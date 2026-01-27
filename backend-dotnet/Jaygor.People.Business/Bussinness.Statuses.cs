using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public IEnumerable<statuses> GetStatuses()
        {
            return dataAccessLayer.GetStatuses();
        }

    }
}
