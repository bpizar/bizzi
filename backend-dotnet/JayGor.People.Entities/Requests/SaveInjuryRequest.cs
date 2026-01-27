using System;
using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;
using JayGor.People.Entities.Requests;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.Requests
{
    public class SaveInjuryRequest : CommonRequest
    {
        public h_injuries Injury { get; set; }
        public List<h_catalogCustom> Catalog { get; set; }
        public List<List<PointBody>> Points { get; set; }

        public int TimeDifference { get; set; }

    }
}
