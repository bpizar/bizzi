using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class common_errors
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }
        public long? IdfUser { get; set; }
    }
}
