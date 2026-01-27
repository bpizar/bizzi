using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public IEnumerable<positions> GetPositions()
        {
            return context.positions.Where(c=>c.State!="D").OrderBy(c=>c.Name).ToList();
        }

		public IEnumerable<positions> GetPositionsForCopyWindow(long idProject, long idperiod)
		{
			var result = new List<positions>();
			var period = context.periods.Where(c => c.Id == idperiod).FirstOrDefault();

			return context.staff_project_position
							.Where(c => c.IdfProject == idProject && c.State != "D" && c.IdfPeriod == idperiod)
                            //.Distinct()
                            .Select(p => new positions
								   {
                                        Id = p.IdfPositionNavigation.Id,											                               
                                        Name = p.IdfPositionNavigation.Name
                                   }).Distinct().ToList();
			
		}

		public CommonResponse SavePositions(List<PositionsCustom> positionss)
		{
            var result = new CommonResponse();
            foreach (var p in positionss.Where(c=>c.Abm!=null))
            {
                switch (p.Abm)
                {
                    case "I":

						var record = context.positions.Where(c => c.Id == p.Id).SingleOrDefault();

						if (record != null)
						{
							record.State = "C";			
						}
                        else
                        {
							var newp = new positions
							{
								Name = p.Name,
								State = "C"
							};

							context.positions.Add(newp);								
                        }
                        								
                        context.SaveChanges();
                                                                                       		                        
                        break;
                        		                   
                    case "D":
                        var forDelete= context.positions.Where(c => c.Id == p.Id).SingleOrDefault();

                        if (forDelete != null)
                        {
                            forDelete.State = "D";
                        }                                

                        break;
                }

                context.SaveChanges();
            }

            return result;
        }
	}
}