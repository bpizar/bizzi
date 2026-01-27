using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Enumerators;
using JayGor.People.Entities.Responses;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public List<ClientCustomEntity> GetClientsByIncident(long idIncident)
        {
            return context.h_clients_incident
                                      .Where(c => c.IdfIncident == idIncident && c.State != "D")
                                      .Select(p => new ClientCustomEntity
                                      {
                                          Id = p.Id,
                                          FullName = string.Format("{0} {1}", p.IdfClientNavigation.LastName, p.IdfClientNavigation.FirstName),
                                          Img = p.IdfClientNavigation.clients_images.Single(D => D.Id == p.IdfClientNavigation.IdfImg).Name,
                                          Abm = "",
                                          IdfClient = p.IdfClientNavigation.Id
                                      }).ToList();
        }

        public IEnumerable<ClientCustomEntity> GetClientsByProjects(long projectId, long idperiod)
        {
            return context.projects_clients
                                    .Where(c => c.IdfProject == projectId && c.IdfPeriod == idperiod && c.State != "D")
                                    .Select(p => new ClientCustomEntity
                                    {
                                        Id = p.Id,
                                        FullName = string.Format("{0} {1}", p.IdfClientNavigation.LastName, p.IdfClientNavigation.FirstName),
                                        Img = p.IdfClientNavigation.clients_images.Single(D => D.Id == p.IdfClientNavigation.IdfImg).Name,
                                        Abm = "",
                                        IdfClient = p.IdfClientNavigation.Id
                                    }).ToList();
        }


        public IEnumerable<ClientCustomEntity> GetClientsByProjectsAllPeriods(long projectId)
        {
            return context.projects_clients
                                .Where(c => c.IdfProject == projectId && c.State != "D")
                                .Select(p => new ClientCustomEntity
                                {
                                    // Id = p.CLIENTX.Id,   
                                    Id = p.Id,
                                    FullName = string.Format("{0} {1}", p.IdfClientNavigation.LastName, p.IdfClientNavigation.FirstName),
                                    Img = p.IdfClientNavigation.clients_images.Single(D => D.Id == p.IdfClientNavigation.IdfImg).Name,
                                    Abm = "",
                                    IdfClient = p.IdfClientNavigation.Id
                                })
                                .ToList();
        }

        public IEnumerable<ClientCustomEntity> GetClients()
        {
            return context.clients
                                    .Where(c => c.Active == 1)
                                    .Select(p => new ClientCustomEntity
                                    {
                                        Id = p.Id,
                                        FirstName = p.FirstName,
                                        LastName = p.LastName,
                                        BirthDate = p.BirthDate,
                                        PhoneNumber = p.PhoneNumber,
                                        Email = p.Email,
                                        Notes = p.Notes,
                                        Active = p.Active,
                                        State = p.State,
                                        FullName = string.Format("{0} {1}", p.LastName, p.FirstName),
                                        Img = context.clients_images.Where(c => c.Id == p.IdfImg).Single().Name,
                                    }).ToList();
        }

        public ClientCustomEntity GetClientById(long id)
        {
            return context.clients
                    .Where(c => c.Id == id)
                                .Select(p => new ClientCustomEntity
                                {
                                    Id = p.Id,
                                    Active = p.Active,
                                    BirthDate = p.BirthDate,
                                    Email = p.Email,
                                    FirstName = p.FirstName,
                                    Img = context.clients_images.Where(c => c.Id == p.IdfImg).Single().Name,
                                    LastName = p.LastName,
                                    Notes = p.Notes,
                                    SafetyPlan = p.SafetyPlan,
                                    PhoneNumber = p.PhoneNumber,
                                    State = p.State,
                                    FullName = string.Format("{0} {1}", p.FirstName, p.LastName),
                                    tmpMotherName = p.tmpMotherName,
                                    tmpMotherInfo = p.tmpMotherInfo,
                                    tmpFatherName = p.tmpFatherName,
                                    tmpFatherInfo = p.tmpFatherInfo,
                                    tmpPlacement = p.tmpPlacement,
                                    tmpSupervisor = p.tmpSupervisor,
                                    tmpSpecialProgram = p.tmpSpecialProgram,
                                    tmpAdditionalInformation = p.tmpAdditionalInformation,
                                    tmpSchool = p.tmpSchool,
                                    tmpSchoolInfo = p.tmpSchoolInfo,
                                    tmpTeacher = p.tmpTeacher,
                                    tmpTeacherInfo = p.tmpTeacherInfo,
                                    tmpDoctorName = p.tmpDoctorName,
                                    tmpDoctorInfo = p.tmpDoctorInfo,
                                    tmpMedicationNotes = p.tmpMedicationNotes,
                                    tmpAgencyWorker = p.tmpAgencyWorker,
                                    tmpAgency = p.tmpAgency,
                                    tmpAgencyInfo = p.tmpAgencyInfo,
                                    tmpAgencyWorkerInfo = p.tmpAgencyWorkerInfo
                                }).Single();
        }


        public List<h_medical_remindersCustom> GetMedicalRemindersByClient(long idClient)
        {
            return context.h_medical_reminders.Where(c => c.State != "D" && c.IdfClient == idClient).Select(x => new h_medical_remindersCustom
            {
                Id = x.Id,
                Color = x.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                ProjectName = x.IdfAssignedToNavigation.IdfProjectNavigation.Description,
                Description = x.Description,
                IdfAssignedTo = x.IdfAssignedTo,
                IdfClient = x.IdfClient,
                Reminder = x.Reminder,
                SppDescription = string.Format("{0} {1} - {2}", x.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, x.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName, x.IdfAssignedToNavigation.IdfPositionNavigation.Name),
                abm = string.Empty,
                Datetime = x.Datetime,
                From = x.From,
                To = x.To,
                State = x.State
            }).ToList();
        }

        public List<h_medical_remindersCustom> GetMedicalRemindersByUser(long idUser)
        {
            return context.h_medical_reminders.Where(c => c.State != "D" && c.IdfAssignedToNavigation.IdfStaffNavigation.IdfUser == idUser && (DateTime.Now >= c.From && DateTime.Now <= c.To)).Select(x => new h_medical_remindersCustom
            {
                Id = x.Id,
                Color = x.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                ProjectName = x.IdfAssignedToNavigation.IdfProjectNavigation.Description,
                Description = string.Format("{0}", x.Description),
                Client = string.Format("{0} {1}", x.IdfClientNavigation.LastName, x.IdfClientNavigation.FirstName),
                IdfAssignedTo = x.IdfAssignedTo,
                IdfClient = x.IdfClient,
                Reminder = x.Reminder,
                SppDescription = string.Format("{0} {1} - {2}", x.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, x.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName, x.IdfAssignedToNavigation.IdfPositionNavigation.Name),
                abm = string.Empty,
                Datetime = x.Datetime,
                From = x.From,
                To = x.To,
                State = x.State
            }).ToList();
        }


        public ClientCustomEntity GetClientByEmail(string email)
        {
            return context.clients
                                    .Where(c => c.Email == email)
                                    .Select(p => new ClientCustomEntity
                                    {
                                        Id = p.Id,
                                        Active = p.Active,
                                        BirthDate = p.BirthDate,
                                        Email = p.Email,
                                        FirstName = p.FirstName,
                                        Img = context.clients_images.Where(c => c.Id == p.IdfImg).Single().Name,
                                        LastName = p.LastName,
                                        Notes = p.Notes,
                                        PhoneNumber = p.PhoneNumber,
                                        State = p.State
                                    }).FirstOrDefault();
        }

        public bool UpdateClientImage(long id, string fileName)
        {
            var newRecord = new clients_images
            {
                Name = fileName,
                IdfClient = id
            };

            context.clients_images.Add(newRecord);
            context.SaveChanges();

            context.clients.Where(c => c.Id == id).Single().IdfImg = newRecord.Id;
            context.SaveChanges();

            return true;
        }

        public CommonResponse SaveClient(clients Client, List<h_medical_remindersCustom> reminders, List<ProjectClientCustomEntity> ProjectClient)
        {
            var result = new CommonResponse();
            var transaction = context.Database.BeginTransaction();

            try
            {
                switch (Client.Id)
                {
                    case (long)AbmEnum.IsNew:

                        var newClient = new clients
                        {
                            FirstName = Client.FirstName,
                            LastName = Client.LastName,
                            BirthDate = Client.BirthDate,
                            PhoneNumber = Client.PhoneNumber,
                            Email = Client.Email,
                            Notes = Client.Notes,
                            SafetyPlan = Client.SafetyPlan,
                            Active = 1,
                            State = "A",
                            tmpSchool = Client.tmpSchool,
                            tmpTeacher = Client.tmpTeacher,
                            tmpDoctorInfo = Client.tmpDoctorInfo,
                            tmpDoctorName = Client.tmpDoctorName,
                            tmpFatherInfo = Client.tmpFatherInfo,
                            tmpFatherName = Client.tmpFatherName,
                            tmpMedicationNotes = Client.tmpMedicationNotes,
                            tmpMotherInfo = Client.tmpMotherInfo,
                            tmpMotherName = Client.tmpMotherName,
                            tmpPlacement = Client.tmpPlacement,
                            tmpSchoolInfo = Client.tmpSchoolInfo,
                            tmpSpecialProgram = Client.tmpSpecialProgram,
                            tmpSupervisor = Client.tmpSupervisor,
                            tmpTeacherInfo = Client.tmpTeacherInfo,
                            tmpAgencyWorker = Client.tmpAgencyWorker,
                            tmpAgencyWorkerInfo = Client.tmpAgencyWorkerInfo,
                            tmpAgency = Client.tmpAgency,
                            tmpAgencyInfo = Client.tmpAgencyInfo
                        };

                        context.clients.Add(newClient);
                        context.SaveChanges();


                        var newClientImg = new clients_images();
                        newClientImg.Name = "generic";
                        newClientImg.IdfClient = newClient.Id;


                        context.clients_images.Add(newClientImg);
                        context.SaveChanges();
                        context.clients.Where(c => c.Id == newClient.Id).Single().IdfImg = newClientImg.Id;
                        context.SaveChanges();

                        context.h_medical_reminders.AddRange(reminders.Select(x => new h_medical_reminders
                        {
                            Id = x.Id,
                            Datetime = x.Datetime,
                            IdfClient = newClient.Id,
                            Description = x.Description,
                            From = x.From,
                            To = x.To,
                            IdfAssignedTo = x.IdfAssignedTo,
                            Reminder = x.Reminder,
                            State = "C"
                        }));


                        //context.projects_clients.Add(ProjectClient);


                        result.TagInfo = string.Format("{0}", newClient.Id);
                        break;

                    default:

                        var editClient = context.clients.Where(c => c.Id == Client.Id).FirstOrDefault();

                        editClient.FirstName = Client.FirstName;
                        editClient.LastName = Client.LastName;
                        editClient.BirthDate = Client.BirthDate;
                        editClient.PhoneNumber = Client.PhoneNumber;
                        editClient.Email = Client.Email;
                        editClient.Notes = Client.Notes;
                        editClient.SafetyPlan = Client.SafetyPlan;
                        editClient.Active = 1;
                        editClient.State = "A";
                        editClient.tmpSchool = Client.tmpSchool;
                        editClient.tmpTeacher = Client.tmpTeacher;
                        editClient.tmpDoctorInfo = Client.tmpDoctorInfo;
                        editClient.tmpDoctorName = Client.tmpDoctorName;
                        editClient.tmpFatherInfo = Client.tmpFatherInfo;
                        editClient.tmpFatherName = Client.tmpFatherName;
                        editClient.tmpMedicationNotes = Client.tmpMedicationNotes;
                        editClient.tmpMotherInfo = Client.tmpMotherInfo;
                        editClient.tmpMotherName = Client.tmpMotherName;
                        editClient.tmpPlacement = Client.tmpPlacement;
                        editClient.tmpSchoolInfo = Client.tmpSchoolInfo;
                        editClient.tmpSpecialProgram = Client.tmpSpecialProgram;
                        editClient.tmpAdditionalInformation = Client.tmpAdditionalInformation;
                        editClient.tmpSupervisor = Client.tmpSupervisor;
                        editClient.tmpTeacherInfo = Client.tmpTeacherInfo;
                        editClient.tmpAgencyWorker = Client.tmpAgencyWorker;
                        editClient.tmpAgencyWorkerInfo = Client.tmpAgencyWorkerInfo;
                        editClient.tmpAgency = Client.tmpAgency;
                        editClient.tmpAgencyInfo = Client.tmpAgencyInfo;


                        //foreach(var p in ProjectClient)
                        //{
                        //    context.projects_clients.Update(new )
                        //}

                        context.projects_clients.UpdateRange(
                               ProjectClient.Select(x => new projects_clients
                               {
                                   Id = x.Id,
                                   IdfClient = Client.Id,
                                   IdfPeriod = x.IdfPeriod,
                                   IdfProject = x.IdfProject,
                                   IdfSPP = x.IdSPP
                               }
                            ).ToList());

                        //context.projects_clients.UpdateRange(projects);



                        foreach (var mr in reminders)
                        {
                            var rmdb = context.h_medical_reminders.Where(c => c.Id == mr.Id).SingleOrDefault();

                            switch (mr.abm)
                            {
                                case "D":
                                    rmdb.State = "D";
                                    break;
                                case "I":

                                    context.h_medical_reminders.Add(new h_medical_reminders
                                    {
                                        Datetime = mr.Datetime,
                                        Description = mr.Description,
                                        From = mr.From,
                                        To = mr.To,
                                        IdfClient = mr.IdfClient,
                                        IdfAssignedTo = mr.IdfAssignedTo,
                                        Reminder = mr.Reminder
                                    });

                                    break;

                                default:
                                    rmdb.Datetime = mr.Datetime;
                                    rmdb.Description = mr.Description;
                                    rmdb.From = mr.From;
                                    rmdb.To = mr.To;
                                    rmdb.IdfAssignedTo = mr.IdfAssignedTo; // ???
                                    rmdb.Reminder = mr.Reminder;
                                    break;
                            }

                        }

                        context.SaveChanges();
                        result.TagInfo = string.Format("{0}", editClient.Id);
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

        public List<DailyLogCustomEntity> GetDailyLogList(long periodId, long clientId)
        {
            return context.h_dailylogs
                                .Where(c => c.IdfPeriod == periodId && c.ClientId == clientId && c.State != "D")
                                .Select(p => new DailyLogCustomEntity
                                {
                                    Id = p.Id,
                                    Color = p.Project.Color,
                                    DateDailyLog = p.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                    Description = string.IsNullOrEmpty(p.Placement) ? string.Empty : p.Placement,
                                    Abm = string.Empty,
                                    ProjectName = p.Project.ProjectName
                                }).ToList();
        }

        public List<ProjectClientCustomEntity> GetProjectClientByIdPeriodIdClient(long idperiod, long idclient)
        {
            return context.projects_clients
                          .Where(c => c.IdfPeriod == idperiod && c.IdfClient == idclient && c.State != "D")
                          .Select(p => new ProjectClientCustomEntity
                          {
                              Id = p.Id,
                              Color = p.IdfProjectNavigation.Color,
                              Description = p.IdfProjectNavigation.ProjectName,
                              State = p.State,
                              abm = string.Empty,
                              IdSPP = p.IdfSPP,
                              IdfProject = p.IdfProject,
                              IdfClient = p.IdfClient,
                              IdfPeriod = p.IdfPeriod
                          }).ToList();
        }

        public List<h_medical_remindersCustom> GetMedicalRemindersByPeriod(long period)
        {
            var periodRow = this.GetPeriod(period);

            if (periodRow != null)
            {
                return context.h_medical_reminders.ToList().Where(c => c.State != "D"
                                                 // && (new DateTime(c.From.Year, c.From.Month, c.From.Day, 0, 0, 1))
                                                 // .CompareTo(new DateTime(periodRow.From.Year, periodRow.From.Month, periodRow.From.Day, 0, 0, 1)) >= 0

                                                 &&
                                                 // (
                                                 // (
                                                 // ((new DateTime(c.From.Year, c.From.Month, c.From.Day, 0, 0, 1))
                                                 // .CompareTo(new DateTime(periodRow.From.Year, periodRow.From.Month, periodRow.From.Day, 0, 0, 1)) >= 0
                                                 // &&
                                                 // (new DateTime(c.From.Year, c.From.Month, c.From.Day, 0, 0, 1))
                                                 // .CompareTo(new DateTime(periodRow.To.Year, periodRow.To.Month, periodRow.To.Day, 0, 0, 1)) <= 0)

                                                 // ||

                                                 // ((new DateTime(c.To.Year, c.To.Month, c.To.Day, 0, 0, 1))
                                                 // .CompareTo(new DateTime(periodRow.To.Year, periodRow.To.Month, periodRow.To.Day, 0, 0, 1)) <= 0
                                                 // &&
                                                 // (new DateTime(c.To.Year, c.To.Month, c.To.Day, 0, 0, 1))
                                                 // .CompareTo(new DateTime(periodRow.From.Year, periodRow.From.Month, periodRow.From.Day, 0, 0, 1)) >= 0)
                                                 // )

                                                 // )

                                                 (
                                                  (new DateTime(c.From.Year, c.From.Month, c.From.Day, 0, 0, 1))
                                                  .CompareTo(new DateTime(periodRow.From.Year, periodRow.From.Month, periodRow.From.Day, 0, 0, 1)) >= 0
                                                 )

                                                 )
                                                .Select(x => new h_medical_remindersCustom
                                                {
                                                    Id = x.Id + 9000000,
                                                    Color = x.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                                                    ProjectName = x.IdfAssignedToNavigation.IdfProjectNavigation.Description,
                                                    Description = string.Format("{0}", x.Description),
                                                    Client = string.Format("{0} {1}", x.IdfClientNavigation.LastName, x.IdfClientNavigation.FirstName),
                                                    IdfAssignedTo = x.IdfAssignedTo,
                                                    IdfClient = x.IdfClient,
                                                    Reminder = x.Reminder,
                                                    SppDescription = string.Format("{0} {1} - {2}", x.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, x.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName, x.IdfAssignedToNavigation.IdfPositionNavigation.Name),
                                                    abm = string.Empty,
                                                    Datetime = x.Datetime,
                                                    From = x.From,
                                                    To = x.To,
                                                    State = x.State,
                                                    Img = x.IdfClientNavigation.clients_images.Where(c => c.Id == x.IdfClientNavigation.IdfImg).Single().Name,
                                                    IdUser = x.IdfAssignedToNavigation.IdfStaffNavigation.Id
                                                }
                ).ToList();
            }
            else
            {
                return new List<h_medical_remindersCustom>();
            }
        }

        public List<h_medical_remindersCustom> GetMedicalRemindersByPeriodAndOwnerProjects(long period)
        {
            var periodRow = this.GetPeriod(period);

            if (periodRow != null)
            {

                return context.h_medical_reminders
                            .Join(context.projects,                                         s => s.IdfAssignedToNavigation.IdfProject,                                         proy => proy.Id,                                         (s, proy) => new { s, proy })                                         .Where(cc => cc.proy.State != "D" && cc.s.State != "D") // && cc.s.IdfPeriod == period                                         .Join(context.project_owners.Where(c => c.State != "D" && c.IdfOwner == 0 && c.IdfPeriod == period),                                 pro => pro.proy.Id,                                 po => po.IdfProject,                                 (pro, po) => new { pro, po })
                         .Where(c => c.pro.s.State != "D" &&
                                                 ((new DateTime(c.pro.s.From.Year, c.pro.s.From.Month, c.pro.s.From.Day, 0, 0, 1))
                                                  .CompareTo(new DateTime(periodRow.From.Year, periodRow.From.Month, periodRow.From.Day, 0, 0, 1)) >= 0
                                                 ))
                                                .Select(x => new h_medical_remindersCustom
                                                {
                                                    Id = x.pro.s.Id + 9000000,
                                                    Color = x.pro.s.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                                                    ProjectName = x.pro.s.IdfAssignedToNavigation.IdfProjectNavigation.Description,
                                                    Description = string.Format("{0}", x.pro.s.Description),
                                                    Client = string.Format("{0} {1}", x.pro.s.IdfClientNavigation.LastName, x.pro.s.IdfClientNavigation.FirstName),
                                                    IdfAssignedTo = x.pro.s.IdfAssignedTo,
                                                    IdfClient = x.pro.s.IdfClient,
                                                    Reminder = x.pro.s.Reminder,
                                                    SppDescription = string.Format("{0} {1} - {2}", x.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, x.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName, x.pro.s.IdfAssignedToNavigation.IdfPositionNavigation.Name),
                                                    abm = string.Empty,
                                                    Datetime = x.pro.s.Datetime,
                                                    From = x.pro.s.From,
                                                    To = x.pro.s.To,
                                                    State = x.pro.s.State,
                                                    Img = x.pro.s.IdfClientNavigation.clients_images.Where(c => c.Id == x.pro.s.IdfClientNavigation.IdfImg).Single().Name,
                                                    IdUser = x.pro.s.IdfAssignedToNavigation.IdfStaffNavigation.Id
                                                }
                ).ToList();
            }
            else
            {
                return new List<h_medical_remindersCustom>();
            }
        }

        public List<h_medical_remindersCustom> GetMedicalRemindersByPeriodAndAssignedToMe(long period, string username)
        {

            var periodRow = this.GetPeriod(period);

            if (periodRow != null)
            {

                var idstaff = context.staff.Where(c => c.IdfUserNavigation.Email == username).FirstOrDefault().Id;

                return context.h_medical_reminders.Where(c => c.State != "D"
                                                 &&
                                                 (
                                                  (new DateTime(c.From.Year, c.From.Month, c.From.Day, 0, 0, 1))
                                                  .CompareTo(new DateTime(periodRow.From.Year, periodRow.From.Month, periodRow.From.Day, 0, 0, 1)) >= 0
                                                 )

                                                 && c.IdfAssignedToNavigation.IdfStaff == idstaff

                                                 )
                                                .Select(x => new h_medical_remindersCustom
                                                {
                                                    Id = x.Id + 9000000,
                                                    Color = x.IdfAssignedToNavigation.IdfProjectNavigation.Color,
                                                    ProjectName = x.IdfAssignedToNavigation.IdfProjectNavigation.Description,
                                                    Description = string.Format("{0}", x.Description),
                                                    Client = string.Format("{0} {1}", x.IdfClientNavigation.LastName, x.IdfClientNavigation.FirstName),
                                                    IdfAssignedTo = x.IdfAssignedTo,
                                                    IdfClient = x.IdfClient,
                                                    Reminder = x.Reminder,
                                                    SppDescription = string.Format("{0} {1} - {2}", x.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.LastName, x.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation.FirstName, x.IdfAssignedToNavigation.IdfPositionNavigation.Name),
                                                    abm = string.Empty,
                                                    Datetime = x.Datetime,
                                                    From = x.From,
                                                    To = x.To,
                                                    State = x.State,
                                                    Img = x.IdfClientNavigation.clients_images.Where(c => c.Id == x.IdfClientNavigation.IdfImg).Single().Name,
                                                    IdUser = x.IdfAssignedToNavigation.IdfStaffNavigation.Id
                                                }
                ).ToList();
            }
            else
            {
                return new List<h_medical_remindersCustom>();
            }
        }

    }
}