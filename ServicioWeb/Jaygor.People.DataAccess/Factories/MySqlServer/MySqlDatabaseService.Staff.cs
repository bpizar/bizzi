using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Enumerators;
using JayGor.People.Entities.Responses;
using Microsoft.EntityFrameworkCore;


namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public IEnumerable<StaffCustomEntity> GetAllStaff()
        {
            return context.staff
                                    .Include(c => c.IdfUserNavigation)
                                    .Select(p => new StaffCustomEntity
                                    {
                                        Email = p.IdfUserNavigation.Email,
                                        FullName = string.Format("{0} {1}", p.IdfUserNavigation.LastName, p.IdfUserNavigation.FirstName),
                                        Id = p.Id,
                                        IdfUser = p.IdfUserNavigation.Id,
                                        PositionName = string.Empty,
                                        Group = string.Empty,
                                        Img = p.IdfUserNavigation.identity_images.Where(c => c.Id == p.IdfUserNavigation.IdfImg).Single().Name
                                    }).ToList();               
        }

        public IEnumerable<StaffCustomEntity> GetStaffsByProject(long idProject)
        {
             return context.staff_project_position
                                            .Where(c => c.IdfProject == idProject && c.State != "D")
                                            .Include(c => c.IdfStaffNavigation).ThenInclude(c => c.IdfUserNavigation)
                                             .Include(c => c.IdfPositionNavigation).Select(p => new StaffCustomEntity
                                             {
                                                 Email = p.IdfStaffNavigation.IdfUserNavigation.Email,
                                                 FullName = string.Format("{0} {1}", p.IdfStaffNavigation.IdfUserNavigation.LastName, p.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                                 Id = p.Id,
                                                 IdfUser = p.IdfStaffNavigation.IdfUserNavigation.Id,
                                                 PositionName = p.IdfPositionNavigation.Name,
                                                 Group = p.IdfPositionNavigation.Name,
                                                 IdfPosition = p.IdfPositionNavigation.Id,
                                                 IdfStaff = p.IdfStaff,
                                                 Hours = p.Hours,
                                                 Abm = string.Empty
                                             }).ToList();              
        }

        public IEnumerable<StaffCustomEntity> GetStaffsForOwnerList(long idProject, long idperiod)
        {
           return context.staff_project_position
                                    .Include(c => c.IdfStaffNavigation).ThenInclude(c => c.IdfUserNavigation).ThenInclude(c => c.identity_users_rol)
                                    .Where(c => c.IdfProject == idProject && c.State != "D" && c.IdfPeriod == idperiod)
                    .Select(p => new StaffCustomEntity
                    {
                        Email = string.Empty,
                        FullName = string.Format("{0} {1}", p.IdfStaffNavigation.IdfUserNavigation.LastName, p.IdfStaffNavigation.IdfUserNavigation.FirstName),
                        Id = p.IdfStaffNavigation.Id,
                        IdfUser = p.IdfStaffNavigation.IdfUserNavigation.Id,
                        PositionName = "", // .user.staff.spp.POSITION.Name,
                        Group = string.Empty,
                        IdfPosition = 0,// p.user.staff.spp.POSITION.Id,
                        IdfStaff = p.IdfStaffNavigation.Id,// p.ar p.user.staff.stf.Id,
                        Hours = 0,
                        Abm = string.Empty
                    }).ToList();
        }

        public IEnumerable<StaffCustomEntity> GetStaffsByProject2(long idProject, long idperiod)
        {
            var period = context.periods.Where(c => c.Id == idperiod).FirstOrDefault();

            return context.staff_project_position
                            .Include(c => c.IdfStaffNavigation).ThenInclude(c => c.IdfUserNavigation).ThenInclude(c => c.identity_users_rol)
                            .Where(c => c.IdfProject == idProject && c.State != "D" && c.IdfPeriod == idperiod)
                                   .Select(p => new StaffCustomEntity
                                   {
                                       Email = p.IdfStaffNavigation.IdfUserNavigation.Email,
                                       FullName = string.Format("{0} {1}", p.IdfStaffNavigation.IdfUserNavigation.LastName, p.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                       Id = p.Id,
                                       IdfUser = p.IdfStaffNavigation.IdfUserNavigation.Id,
                                       PositionName = p.IdfPositionNavigation.Name,
                                       Group = p.IdfPositionNavigation.Name,
                                       IdfPosition = p.IdfPositionNavigation.Id,
                                       IdfStaff = p.IdfStaff,
                                       Hours = 0, //Convert.ToDecimal(context.scheduling
                                                  //        .Where(c => c.IdfAssignedTo == p.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                                  //               .Sum(c =>Convert.ToDateTime(c.To).Subtract(Convert.ToDateTime(c.From)).TotalSeconds)),
                                       Abm = string.Empty,
                                       State = p.State,
                                       Img = p.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == p.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name
                                   }).ToList();
        }

        public IEnumerable<StaffCustomEntity> GetStaffs()
        {
            var result = new List<StaffCustomEntity>();
           
            var staffsList = context.staff
                                    .Include(c => c.staff_project_position)
                   .ThenInclude(c => c.IdfPositionNavigation)
                                    .Include(c => c.IdfUserNavigation)
                                    .ThenInclude(x => x.identity_images);

            foreach (var s in staffsList)
            {
                var positions = string.Empty;
                var positionsList = new List<string>();

                foreach (var sp in s.staff_project_position)
                {
                    if (!positionsList.Contains(sp.IdfPositionNavigation.Name))
                    {
                        positions += sp.IdfPositionNavigation.Name + " ";
                        positionsList.Add(sp.IdfPositionNavigation.Name);
                    }
                }

                var newStaff = new StaffCustomEntity
                {
                    Id = s.Id,
                    IdfUser = s.IdfUser,
                    Email = s.IdfUserNavigation.Email,
                    FullName = string.Format("{0} {1}", s.IdfUserNavigation.LastName, s.IdfUserNavigation.FirstName),
                    PositionName = positions,
                    Img = s.IdfUserNavigation.identity_images.Single(c => c.Id == s.IdfUserNavigation.IdfImg).Name,
                    HomePhone = s.HomePhone,
                    CellNumber = s.CellNumber,
                    City = s.City
                };

                result.Add(newStaff);
            }
                          
            return result;
        }

        public StaffCustomEntity GetStaffbyId(long id)
        {
            return context.staff
                        .Where(c => c.Id == id)    
                                    .Include(c=>c.IdfUserNavigation)
                                    .Select(p => new StaffCustomEntity
                                    {
                                        Id = p.Id,
                                        Color = p.Color,
                                        IdfUser = p.IdfUser,
                                        project_owners = null,// p.project_owners,
                                                staff_project_position = null,// p.staff_project_position,// este da el problema
                                                State = p.State,
                                        IdfUserNavigation = p.IdfUserNavigation,
                                        GeoTrackingEvery = p.IdfUserNavigation.GeoTrackingEvery,
                                        Img = p.IdfUserNavigation.identity_images.Single(c => c.Id == p.IdfUserNavigation.IdfImg).Name,
                                        AvailableForManyPrograms = p.AvailableForManyPrograms,
                                        CellNumber = p.CellNumber,
                                        EmergencyPerson = p.EmergencyPerson,
                                        EmergencyPersonInfo = p.EmergencyPersonInfo,
                                        HealthInsuranceNumber = p.HealthInsuranceNumber,
                                        HomeAddress = p.HomeAddress,
                                        HomePhone = p.HomePhone,
                                        SocialInsuranceNumber = p.SocialInsuranceNumber,
                                        SpouceName = p.SpouceName,
                                        tmpAccreditations = p.tmpAccreditations,
                                        WorkStartDate = p.WorkStartDate,
                                        City = p.City
                                    }).Single();                    
        }


        public CommonResponse SaveStaff(staff Staff,
                                        identity_users user,
                                        List<long> roles,
                                        string password,
                                        int workingHoursByPeriodStaff,
                                        long idfPeriod)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();

          
            try
            {
                switch (Staff.Id)
                {
                    case (long)AbmEnum.IsNew:

                        var newUser = new identity_users
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Password = password,
                            State = "A",
                            GeoTrackingEvery = user.GeoTrackingEvery
                        };

                        context.identity_users.Add(newUser);

                        context.SaveChanges();

                        var identityImage = new identity_images
                        {
                            //Id = newUser.Id,
                            Name = "generic",
                            IdfIdentity_user = newUser.Id
                        };

                        context.identity_images.Add(identityImage);

                        context.SaveChanges();

                        context.identity_users.Where(c => c.Id == newUser.Id).Single().IdfImg = identityImage.Id;

                        var roluser = context.identity_roles.Where(c => c.Rol.ToLower() == "user" && c.State != "D").Single();

                        if (roluser != null)
                        {
                            var newuserrol = new identity_users_rol
                            {
                                IdfRol = roluser.Id,
                                IdfUser = newUser.Id,
                                State = "A"
                            };

                            context.identity_users_rol.Add(newuserrol);
                        }

                        var newStaff = new staff
                        {
                            IdfUser = newUser.Id,
                            State = "A",
                            Color = string.Empty,

                            AvailableForManyPrograms = Staff.AvailableForManyPrograms,
                            CellNumber = Staff.CellNumber,
                            EmergencyPerson = Staff.EmergencyPerson,
                            EmergencyPersonInfo = Staff.EmergencyPersonInfo,
                            HealthInsuranceNumber = Staff.HealthInsuranceNumber,
                            HomeAddress = Staff.HomeAddress,
                            HomePhone = Staff.HomePhone,
                            SocialInsuranceNumber = Staff.SocialInsuranceNumber,
                            SpouceName = Staff.SpouceName,
                            tmpAccreditations = Staff.tmpAccreditations,
                            WorkStartDate = Staff.WorkStartDate,
                            City = Staff.City

                        };

                        context.staff.Add(newStaff);
                        context.SaveChanges();

                        result.TagInfo = string.Format("{0}-{1}", newUser.Id, newStaff.Id);
                        
                        var newSset = new staff_period_settings()
                        {

                            IdfPeriod = idfPeriod, // lastPeriod.Id, // newPeriod.Id,
                            IdfStaff = newStaff.Id,
                            WorkingHours = workingHoursByPeriodStaff
                        };

                        context.staff_period_settings.Add(newSset);
                        context.SaveChanges();

                        break;

                    default:
                        var staffdb = context.staff.Where(c => c.Id == Staff.Id).FirstOrDefault();

                        staffdb.AvailableForManyPrograms = Staff.AvailableForManyPrograms;
                        staffdb.CellNumber = Staff.CellNumber;
                        staffdb.EmergencyPerson = Staff.EmergencyPerson;
                        staffdb.EmergencyPersonInfo = Staff.EmergencyPersonInfo;
                        staffdb.HealthInsuranceNumber = Staff.HealthInsuranceNumber;
                        staffdb.HomeAddress = Staff.HomeAddress;
                        staffdb.HomePhone = Staff.HomePhone;
                        staffdb.SocialInsuranceNumber = Staff.SocialInsuranceNumber;
                        staffdb.SpouceName = Staff.SpouceName;
                        staffdb.tmpAccreditations = Staff.tmpAccreditations;
                        staffdb.WorkStartDate = Staff.WorkStartDate;
                        staffdb.City = Staff.City;


                        var userdb = context.identity_users.Where(c => c.Id == user.Id).FirstOrDefault();

                        userdb.FirstName = user.FirstName;
                        userdb.LastName = user.LastName;
                        userdb.State = user.State;
                        userdb.GeoTrackingEvery = user.GeoTrackingEvery;

                        foreach (var rtd in context.identity_users_rol.Where(c => c.IdfUser == user.Id && c.IdfRolNavigation.Rol.ToLower() != "user"))
                        {
                            //context.Identity_Users_Rol.Where(c=>c.Id == rtd.Id).Single().State = "D";
                            rtd.State = "D";
                        }

                        context.SaveChanges();

                        if (roles != null)
                        {
                            foreach (var r in roles)
                            {
                                var record = context.identity_users_rol
                                                    .Include(c => c.IdfRolNavigation)
                                                    .Where(c => c.IdfUser == user.Id && c.IdfRol == r).FirstOrDefault();

                                if (record != null)
                                {
                                    if (record.IdfRolNavigation.Rol.ToLower() != "user")
                                    {
                                        record.State = "A";
                                    }
                                }
                                else
                                {
                                    var newRecord = new identity_users_rol()
                                    {
                                        IdfRol = r,
                                        IdfUser = user.Id,
                                        State = "A"
                                    };

                                    context.identity_users_rol.Add(newRecord);
                                }
                            }
                        }

                        context.SaveChanges();
                        break;
                }

                transaction.Commit();
                result.Result = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }                    
               
            return result;
        }


        // DEPRECAR???? 
        public IEnumerable<StaffForPlanningCustomEntity> GetStaffForPlanning(string groupby)
        {
           return context
                        .staff_project_position
                        .Where(c => c.IdfProjectNavigation.State != "D")
                        .Select(s => new StaffForPlanningCustomEntity
                        {
                            IdfUser = s.IdfStaffNavigation.IdfUserNavigation.Id,
                            IdfProject = s.IdfProject,
                            IdfStaff = s.IdfStaffNavigation.Id,
                            ProjectName = s.IdfProjectNavigation.ProjectName,
                            FullUserName = string.Format("{0} {1}", s.IdfStaffNavigation.IdfUserNavigation.LastName, s.IdfStaffNavigation.IdfUserNavigation.FirstName),
                            PositionName = s.IdfPositionNavigation.Name,
                            Color = s.IdfProjectNavigation.Color,
                            IdfStaffProjectPosition = s.Id,
                            group = string.Format("{0}|{1}", s.IdfProjectNavigation.Color, s.IdfProjectNavigation.ProjectName)
                        }).ToList();               
        }

        public IEnumerable<StaffForPlanningCustomEntity> GetStaffForScheduling(long idperiod)
        {
            var period = context.periods.Where(c => c.Id == idperiod).FirstOrDefault();

            return context.staff_project_position
                .Include(x => x.IdfStaffNavigation)
                .Include(x => x.IdfStaffNavigation.IdfUserNavigation)
                .Include(x => x.IdfStaffNavigation.IdfUserNavigation.identity_images)
                .Include(x => x.IdfProjectNavigation)
                .Include(x => x.IdfPositionNavigation)
                .ToList()
                            .Join(context.projects,
                                   spp => spp.IdfProject,
                                   proy => proy.Id,
                                   (spp, proy) => new { spp, proy })
                            .Where(cc => cc.spp.IdfPeriod == idperiod && cc.proy.State != "D" && cc.spp.State != "D" && (cc.proy.State != "CL" || period.State == "CL"))
                              .Select(s => new StaffForPlanningCustomEntity
                              {
                                  IdfUser = s.spp.IdfStaffNavigation.IdfUserNavigation.Id,
                                  IdfProject = s.spp.IdfProject,
                                  IdfStaff = s.spp.IdfStaffNavigation.Id,
                                  ProjectName = s.proy.ProjectName,
                                  FullUserName = string.Format("{0} {1}", s.spp.IdfStaffNavigation.IdfUserNavigation.LastName, s.spp.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                  PositionName = s.spp.IdfPositionNavigation.Name,
                                  Color = s.spp.IdfProjectNavigation.Color,
                                  IdfStaffProjectPosition = s.spp.Id,//OJO
                                  group = string.Format("{0}|{1}", s.spp.IdfProjectNavigation.Color, s.spp.IdfProjectNavigation.ProjectName),
                                  Hours = Convert.ToInt64(context.scheduling.ToList()
                                .Where(c => c.IdfAssignedTo == s.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                    .Sum(c => Convert.ToDateTime(c.To).Subtract(Convert.ToDateTime(c.From)).TotalSeconds)),
                                  HoursAssigned = context.tasks.ToList()
                                                        .Where(c => c.IdfAssignedTo == s.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                                        .Sum(c => c.Hours),
                                  // when staff_period_settings is null will throw.
                                  MaxHoursByPeriod = s.spp.IdfStaffNavigation.staff_period_settings.FirstOrDefault(c => c.IdfPeriod == idperiod) != null ? s.spp.IdfStaffNavigation.staff_period_settings.FirstOrDefault(c => c.IdfPeriod == idperiod).WorkingHours * 3600 : 0,
                                  Img = s.spp.IdfStaffNavigation.IdfUserNavigation.identity_images.Single(c => c.Id == s.spp.IdfStaffNavigation.IdfUserNavigation.IdfImg).Name
                              }).ToList();                              
        }

        public IEnumerable<StaffCustomEntity> GetStaffForIncident(long idperiod)
        {
            var period = context.periods.Where(c => c.Id == idperiod).FirstOrDefault();

            return context.staff_project_position
                                    .Join(context.projects,
                                           spp => spp.IdfProject,
                                           proy => proy.Id,
                                           (spp, proy) => new { spp, proy })
                                    .Where(cc => cc.spp.IdfPeriod == idperiod && cc.proy.State != "D" && cc.spp.State != "D" && (cc.proy.State != "CL" || period.State == "CL"))
                                      .Select(s => new StaffCustomEntity
                                      {
                                          Id = s.spp.Id,
                                          IdfUser = s.spp.IdfStaffNavigation.IdfUserNavigation.Id,
                                          //IdfProject = s.spp.IdfProject,
                                          IdfStaff = s.spp.IdfStaffNavigation.Id,
                                          //ProjectName = s.proy.ProjectName,
                                          FullName = string.Format("{0} {1} : {2}", s.spp.IdfStaffNavigation.IdfUserNavigation.LastName, s.spp.IdfStaffNavigation.IdfUserNavigation.FirstName, s.spp.IdfPositionNavigation.Name),
                                          PositionName = s.spp.IdfPositionNavigation.Name,
                                          Color = s.spp.IdfProjectNavigation.Color,
                                          Group = s.spp.IdfProjectNavigation.ProjectName,
                                          //IdfStaffProjectPosition = s.spp.Id,//OJO
                                          //group = string.Format("{0}|{1}", s.spp.IdfProjectNavigation.Color, s.spp.IdfProjectNavigation.ProjectName),
                                          //Hours = Convert.ToInt64(context.scheduling
                                          //                          .Where(c => c.IdfAssignedTo == s.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                          //    .Sum(c => Convert.ToDateTime(c.To).Subtract(Convert.ToDateTime(c.From)).TotalSeconds)),
                                          //HoursAssigned = context.tasks
                                          //                    .Where(c => c.IdfAssignedTo == s.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                          //                    .Sum(c => c.Hours),
                                          //MaxHoursByPeriod = s.spp.IdfStaffNavigation.staff_period_settings.Where(c => c.IdfPeriod == idperiod).FirstOrDefault().WorkingHours * 3600,
                                          Img = s.spp.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.spp.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name
                                      }).ToList();               
        }

        public IEnumerable<StaffCustomEntity> GetStaffForDailyLog(long idperiod)
        {
            var period = context.periods.Where(c => c.Id == idperiod).FirstOrDefault();

            return context.staff_project_position
                            .Join(context.projects,
                                   spp => spp.IdfProject,
                                   proy => proy.Id,
                                   (spp, proy) => new { spp, proy })
                            .Where(cc => cc.spp.IdfPeriod == idperiod && cc.proy.State != "D" && cc.spp.State != "D" && (cc.proy.State != "CL" || period.State == "CL"))
                              .Select(s => new StaffCustomEntity
                              {
                                  Id = s.spp.Id,
                                  IdfUser = s.spp.IdfStaffNavigation.IdfUserNavigation.Id,
                                  //IdfProject = s.spp.IdfProject,
                                  IdfStaff = s.spp.IdfStaffNavigation.Id,
                                  //ProjectName = s.proy.ProjectName,
                                  FullName = string.Format("{0} {1} : {2}", s.spp.IdfStaffNavigation.IdfUserNavigation.LastName, s.spp.IdfStaffNavigation.IdfUserNavigation.FirstName, s.spp.IdfPositionNavigation.Name),
                                  PositionName = s.spp.IdfPositionNavigation.Name,
                                  Color = s.spp.IdfProjectNavigation.Color,
                                  Group = s.spp.IdfProjectNavigation.ProjectName,
                                  //IdfStaffProjectPosition = s.spp.Id,//OJO
                                  //group = string.Format("{0}|{1}", s.spp.IdfProjectNavigation.Color, s.spp.IdfProjectNavigation.ProjectName),
                                  //Hours = Convert.ToInt64(context.scheduling
                                  //                          .Where(c => c.IdfAssignedTo == s.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                  //    .Sum(c => Convert.ToDateTime(c.To).Subtract(Convert.ToDateTime(c.From)).TotalSeconds)),
                                  //HoursAssigned = context.tasks
                                  //                    .Where(c => c.IdfAssignedTo == s.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                  //                    .Sum(c => c.Hours),
                                  //MaxHoursByPeriod = s.spp.IdfStaffNavigation.staff_period_settings.Where(c => c.IdfPeriod == idperiod).FirstOrDefault().WorkingHours * 3600,
                                  Img = s.spp.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.spp.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name
                              }).ToList();
                                                     
        }

        public IEnumerable<StaffForGeoTrackingCustomEntity> GetStaffForGeoTracking(long idperiod)
        {
            var period = context.periods.Where(c => c.Id == idperiod).FirstOrDefault();

            return context.staff_project_position
                            .Join(context.projects,
                                   spp => spp.IdfProject,
                                   proy => proy.Id,
                                   (spp, proy) => new { spp, proy })
                            .Where(cc => cc.spp.IdfPeriod == idperiod && cc.proy.State != "D" && cc.spp.State != "D" && (cc.proy.State != "CL" || period.State == "CL"))
                              .Select(s => new StaffForGeoTrackingCustomEntity
                              {
                                  IdfUser = s.spp.IdfStaffNavigation.IdfUserNavigation.Id,
                                  IdfProject = s.spp.IdfProject,
                                  IdfStaff = s.spp.IdfStaffNavigation.Id,
                                  ProjectName = s.proy.ProjectName,
                                  FullUserName = string.Format("{0} {1}", s.spp.IdfStaffNavigation.IdfUserNavigation.LastName, s.spp.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                  PositionName = s.spp.IdfPositionNavigation.Name,
                                  Color = s.spp.IdfProjectNavigation.Color,
                                  IdfStaffProjectPosition = s.spp.Id,//OJO
                                  group = string.Format("{0}|{1}", s.spp.IdfProjectNavigation.Color, s.spp.IdfProjectNavigation.ProjectName),
                                  Hours = 0, // Convert.ToDecimal(context.scheduling
                                             //			 .Where(c => c.IdfAssignedTo == s.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                             //			.Sum(c => Convert.ToDateTime(c.To).Subtract(Convert.ToDateTime(c.From)).TotalSeconds)),
                                  Img = s.spp.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.spp.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name
                              }).ToList();
        }

        public IEnumerable<StaffForPlanningCustomEntity> GetStaffByOwnProjects(long idperiod, string username)
        {
            var period = context.periods.Where(c => c.Id == idperiod).FirstOrDefault();
            var idstaff = context.staff.Where(c => c.IdfUserNavigation.Email == username).FirstOrDefault().Id;
            return context.staff_project_position
                            .Join(context.projects,
                                   spp => spp.IdfProject,
                                   proy => proy.Id,
                                   (spp, proy) => new { spp, proy })
                            .Where(cc => cc.spp.IdfPeriod == idperiod && cc.proy.State != "D" && cc.spp.State != "D" && (cc.proy.State != "CL" || period.State == "CL"))
                             .Join(context.project_owners,
                                       pro => pro.proy.Id,
                                       po => po.IdfProject,
                                       (pro, po) => new { pro, po })
                            .Where(cc => cc.po.State != "D" && cc.po.IdfOwner == idstaff && cc.po.IdfPeriod == idperiod)
                              .Select(s => new StaffForPlanningCustomEntity
                              {
                                  IdfUser = s.pro.spp.IdfStaffNavigation.IdfUserNavigation.Id,
                                  IdfProject = s.pro.spp.IdfProject,
                                  IdfStaff = s.pro.spp.IdfStaffNavigation.Id,
                                  ProjectName = s.pro.spp.IdfProjectNavigation.ProjectName,
                                  FullUserName = string.Format("{0} {1}", s.pro.spp.IdfStaffNavigation.IdfUserNavigation.LastName, s.pro.spp.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                  PositionName = s.pro.spp.IdfPositionNavigation.Name,
                                  Color = s.pro.spp.IdfProjectNavigation.Color,
                                  IdfStaffProjectPosition = s.pro.spp.Id,
                                  group = string.Format("{0}|{1}", s.pro.spp.IdfProjectNavigation.Color, s.pro.spp.IdfProjectNavigation.ProjectName),


                                  //Hours = Convert.ToInt64(context.scheduling
                                  //					 .Where(c => c.IdfAssignedTo == s.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                  //.Sum(c => Convert.ToDateTime(c.To).Subtract(Convert.ToDateTime(c.From)).TotalSeconds)),
                                  HoursAssigned = context.tasks
                                                         .Where(c => c.IdfAssignedTo == s.pro.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                                        .Sum(c => c.Hours),
                                  MaxHoursByPeriod = s.pro.spp.IdfStaffNavigation.staff_period_settings.Where(c => c.IdfPeriod == idperiod).FirstOrDefault().WorkingHours * 3600,

                                  // Img = s.spp.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.spp.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name

                                  Hours = Convert.ToInt32(context.scheduling
                                                        .Where(c => c.IdfAssignedTo == s.pro.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                                        .Sum(c => Convert.ToDateTime(c.To).Subtract(Convert.ToDateTime(c.From)).TotalSeconds)),
                                  Img = context.identity_images.Where(c => c.Id == s.pro.spp.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name


                                  //Img = s.spp.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.spp.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name

                              }).ToList();
        }

        public IEnumerable<StaffForPlanningCustomEntity> GetStaffByOwnScheduling(long idperiod, string username)
        {
                var period = context.periods.Where(c => c.Id == idperiod).FirstOrDefault();
                var idstaff = context.staff.Where(c => c.IdfUserNavigation.Email == username).FirstOrDefault().Id;

               return context.staff_project_position
                                .Join(context.projects,
                                       spp => spp.IdfProject,
                                       proy => proy.Id,
                                       (spp, proy) => new { spp, proy })
                                .Where(cc => cc.spp.IdfPeriod == idperiod && cc.proy.State != "D" && cc.spp.State != "D" && (cc.proy.State != "CL" || period.State == "CL"))
                                  .Select(s => new StaffForPlanningCustomEntity
                                  {
                                      IdfUser = s.spp.IdfStaffNavigation.IdfUserNavigation.Id,
                                      IdfProject = s.spp.IdfProject,
                                      IdfStaff = s.spp.IdfStaffNavigation.Id,
                                      ProjectName = s.spp.IdfProjectNavigation.ProjectName,
                                      FullUserName = string.Format("{0} {1}", s.spp.IdfStaffNavigation.IdfUserNavigation.LastName, s.spp.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                      PositionName = s.spp.IdfPositionNavigation.Name,
                                      Color = s.spp.IdfProjectNavigation.Color,
                                      IdfStaffProjectPosition = s.spp.Id,
                                      group = string.Format("{0}|{1}", s.spp.IdfProjectNavigation.Color, s.spp.IdfProjectNavigation.ProjectName),
                                      Hours = Convert.ToInt32(context.scheduling
                                                             .Where(c => c.IdfAssignedTo == s.spp.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                                            .Sum(c => Convert.ToDateTime(c.To).Subtract(Convert.ToDateTime(c.From)).TotalSeconds)),
                                      Img = s.spp.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == s.spp.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name
                                  }).
                                  Where(cc => cc.IdfStaff == idstaff)
                                  .ToList();
        }

        public bool ResetPassword(long id, string newPassword)
        {           
            var user = this.IdentityGetUserById(id);
            user.Password = newPassword;
            context.identity_users.Update(user);
            context.SaveChanges();
           
            return true;
        }

        public CommonResponse EnableDisableAccount(long IdUser, string State)
        {
            var result = new CommonResponse();

            context.identity_users.Where(c => c.Id == IdUser).Single().State = State;
            // context.staff.Where(c => c.State != "D" && c.IdfUser == IdUser).ToList().ForEach(c => c.State = State);
            context.staff.Where(c => c.IdfUser == IdUser).ToList().ForEach(c => c.State = State);
            context.SaveChanges();
            return result;
        }

        public IEnumerable<StaffCustomEntity> GetStaffForCopyWindow(long idProject, long idperiod)
        {    
            var period = context.periods.Where(c => c.Id == idperiod).FirstOrDefault();

            return context.staff_project_position
                            .Where(c => c.IdfProject == idProject && c.State != "D" && c.IdfPeriod == idperiod)
                                   .Select(p => new StaffCustomEntity
                                   {
                                       Email = p.IdfStaffNavigation.IdfUserNavigation.Email,
                                       FullName = string.Format("{0} {1}", p.IdfStaffNavigation.IdfUserNavigation.LastName, p.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                       Id = p.Id,
                                       IdfUser = p.IdfStaffNavigation.IdfUserNavigation.Id,
                                       PositionName = p.IdfPositionNavigation.Name,
                                       Group = p.IdfPositionNavigation.Name,
                                       IdfPosition = p.IdfPositionNavigation.Id,
                                       IdfStaff = p.IdfStaff,
                                       Hours = 0, //Convert.ToDecimal(context.scheduling
                                                  //        .Where(c => c.IdfAssignedTo == p.Id && c.State != "D" && c.IdfPeriod == idperiod)
                                                  //               .Sum(c => Convert.ToDateTime(c.To).Subtract(Convert.ToDateTime(c.From)).TotalSeconds)),
                                       Abm = string.Empty,
                                       State = p.State,
                                       Img = p.IdfStaffNavigation.IdfUserNavigation.identity_images.Where(c => c.Id == p.IdfStaffNavigation.IdfUserNavigation.IdfImg).Single().Name
                                   }).ToList();
        }

        public staff_period_settings GetStaffPeriodSettings(long idStaff, long idPeriod)
        {
           return context.staff_period_settings.Where(c => c.IdfStaff == idStaff && c.IdfPeriod == idPeriod).FirstOrDefault();                
        }

        public bool SaveStaffPeriodSettings(long idStaff, long idPeriod, int hours)
        {
            var transaction = context.Database.BeginTransaction();
                       
            try
            {
                var item = this.GetStaffPeriodSettings(idStaff, idPeriod);

                if (item == null)
                {
                    var newItem = new staff_period_settings
                    {
                        IdfStaff = idStaff,
                        IdfPeriod = idPeriod,
                        WorkingHours = hours
                    };

                    context.staff_period_settings.Add(newItem);
                }
                else
                {
                    context.staff_period_settings.Where(c => c.IdfStaff == idStaff && c.IdfPeriod == idPeriod).FirstOrDefault().WorkingHours = hours; // .ToList().ForEach(c => c.State = State);
                }



                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return true;
        }

        public List<StaffCustomEntity> GetStaffsByClient(long idClient, long idperiod = 0)
        {
             var idperiodAux = idperiod == 0 ? context.periods.OrderByDescending(c => c.Id).First().Id : idperiod;
          

             if (idperiodAux > 0)
             {
                return context.staff_project_position
                    .Join(context.projects, spp => spp.IdfProject, pro => pro.Id, (spp, pro) => new { SPP = spp, Pro = pro })
                           .Where(c => c.SPP.State != "D" && c.SPP.IdfPeriod == idperiodAux && c.SPP.IdfProjectNavigation.projects_clients.Where(x=>x.IdfClient == idClient).Any())
                                  .Select(p => new StaffCustomEntity
                                  {
                                      Email = p.SPP.IdfStaffNavigation.IdfUserNavigation.Email,
                                      FullName = string.Format("{0} {1}", p.SPP.IdfStaffNavigation.IdfUserNavigation.LastName, p.SPP.IdfStaffNavigation.IdfUserNavigation.FirstName),
                                      Id = p.SPP.Id,
                                      IdfUser = p.SPP.IdfStaffNavigation.IdfUserNavigation.Id,
                                      PositionName = p.SPP.IdfPositionNavigation.Name,
                                      Group = p.SPP.IdfPositionNavigation.Name,
                                      IdfPosition = p.SPP.IdfPositionNavigation.Id,
                                      IdfStaff = p.SPP.IdfStaff,
                                      Hours = 0,
                                      Abm = string.Empty,
                                      ProjectInfo = p.Pro.ProjectName,
                                      ProjectColor = p.Pro.Color,
                                      State = p.SPP.State,
                                      Img = p.SPP.IdfStaffNavigation.IdfUserNavigation.identity_images.Single(c => c.Id == p.SPP.IdfStaffNavigation.IdfUserNavigation.IdfImg).Name
                                  }).ToList();
             }
             else
             {
                return new List<StaffCustomEntity>();
             }
        }

    }
}