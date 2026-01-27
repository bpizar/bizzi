using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;


namespace JayGor.People.Entities.Responses
{
    public class GetIncidentsListByPeriodResponse : CommonResponse
    {
        public List<IncidentCustomEntity> Incidents { get; set; } = new List<IncidentCustomEntity>();
    }
}
