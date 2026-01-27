using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class StaffFormbyStaffCustomEntity : staff_forms
    {
        public long IdfStaffFormValue { get; set; }

        public string FormDateTime { get; set; }

        public string NextDate { get; set; }

        public int quantity { get; set; }
    }
}
