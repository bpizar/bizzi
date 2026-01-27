using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class ClientFormbyClientCustomEntity : client_forms
    {
        public long IdfClientFormValue { get; set; }
        
        public string FormDateTime { get; set; }

        public int quantity { get; set; }
    }
}
