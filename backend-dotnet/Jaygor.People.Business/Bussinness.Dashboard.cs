
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.Responses;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public List<GenericPair> GetTasksForDashboard1(out string periodDescription)
        {
            return dataAccessLayer.GetTasksForDashboard1(out periodDescription);
        }

        public void GetDashboard2(out string periodDescription, out long max, out List<string> colors, out List<long> values, out List<string> projectNames)
        {
            colors = new List<string>();
            values = new List<long>();
            projectNames = new List<string>();
            max = 0;

            var auxtasks = dataAccessLayer.GetDashboard2(out periodDescription);

            var auxIdProjects = new List<long>();

            foreach (var x in auxtasks)
            {
                var projectIdAux = Convert.ToInt64(x.IdfProject);

                if (!auxIdProjects.Contains(projectIdAux))
                {
                    auxIdProjects.Add(projectIdAux);

                    projectNames.Add(x.IdfProjectNavigation.ProjectName);
                    colors.Add(string.Format("#{0}", x.IdfProjectNavigation.Color));
                    values.Add(auxtasks.Where(c => c.IdfProject == projectIdAux).Count());

                }

                max = values.Max();
            }




        }




        public List<GenericTriValue> GetDashboard3(string username, out string periodDescription)
		{		
			return dataAccessLayer.GetDashboard3(username,out periodDescription);
		}



      


    }




}














//var values = auxtasks
//.GroupBy(c => c.IdfProjectNavigation.ProjectName)
//.Select(p => new GenericPair
//{
//    Id = p.Key,
//    Description = p.Count().ToString(),                                                   
//});


//var xxvalues = auxtasks
//        .GroupBy(c => c.IdfProjectNavigation.ProjectName)
//.Select(c=>c.Count().ToString()).ToList();

//xxvalues.First()


//xxvalues.First()

//                  .Select(p => new string
//{
//  //Id = p.Key,
//  //Description = p.Count().ToString(),
//                      "sdf"
//});

