using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class ClientCustomEntity: clients
    {
        //public String ProjectName { get; set; }
        public string FullName { get; set; }
        public string Img { get; set; }

        public string Abm { get; set; }
        public long IdfClient { get; set; }


        public string ProgramInfo { get; set; }
    }
}


