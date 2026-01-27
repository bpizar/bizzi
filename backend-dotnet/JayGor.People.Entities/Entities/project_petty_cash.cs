using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class project_petty_cash
    {
        public long Id { get; set; }
        public long IdfProject { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string State { get; set; }
        public long IdfPeriod { get; set; }
        public long IdfCategories { get; set; }

        public virtual project_pettycash_categories IdfCategoriesNavigation { get; set; }
        public virtual periods IdfPeriodNavigation { get; set; }
        public virtual projects IdfProjectNavigation { get; set; }
    }
}
