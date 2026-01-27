//using JayGor.People.DataAccess.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using Microsoft.EntityFrameworkCore;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public IEnumerable<ProjectPositionCustomEntity> GetProjectPosisionCustom(long idStaff)
        {
            return context.staff_project_position
                        .Where(c => c.IdfStaff == idStaff)
                                    .Include(t => t.IdfProjectNavigation)
                                    .Include(t => t.IdfPositionNavigation)
                        .Select(p => new ProjectPositionCustomEntity
                        {
                            Id = p.Id,
                            PositionName = p.IdfPositionNavigation.Name,
                            ProjectName = p.IdfProjectNavigation.ProjectName,
                            Group = p.IdfProjectNavigation.ProjectName,
                            IdPosition = p.IdfPosition,
                            IdProject = p.IdfProject
                        }).ToList();
        }

    }
}