using System;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;

namespace JayGor.People.Entities.Responses
{
    public class getUserProjectsPositionsResponse : CommonResponse
    {
		public List<StaffProjectPositionCustomEntity> staffProjectPositions { get; set; }
    }
}