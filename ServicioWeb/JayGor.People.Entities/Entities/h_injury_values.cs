using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class h_injury_values
    {
        public long id { get; set; }
        public long idfInjury { get; set; }
        public string idfCatalog { get; set; }
        public string Value { get; set; }

        public virtual h_catalog idfCatalogNavigation { get; set; }
        public virtual h_injuries idfInjuryNavigation { get; set; }
    }
}
