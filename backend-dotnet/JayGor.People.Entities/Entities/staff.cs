using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class staff
    {
        public staff()
        {
            project_owners = new HashSet<project_owners>();
            staff_period_settings = new HashSet<staff_period_settings>();
            staff_project_position = new HashSet<staff_project_position>();
        }

        public long Id { get; set; }
        public long IdfUser { get; set; }
        public string State { get; set; }
        public string Color { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public string SocialInsuranceNumber { get; set; }
        public string HealthInsuranceNumber { get; set; }
        public string HomeAddress { get; set; }
        public string City { get; set; }
        public string HomePhone { get; set; }
        public string CellNumber { get; set; }
        public string SpouceName { get; set; }
        public string EmergencyPerson { get; set; }
        public string EmergencyPersonInfo { get; set; }
        public int? AvailableForManyPrograms { get; set; }
        public string tmpAccreditations { get; set; }

        public virtual identity_users IdfUserNavigation { get; set; }
        public virtual ICollection<project_owners> project_owners { get; set; }
        public virtual ICollection<staff_period_settings> staff_period_settings { get; set; }
        public virtual ICollection<staff_project_position> staff_project_position { get; set; }
    }
}
