using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {

        public bool GetDailyLogById(long idDailyLog,
                                    long idPeriod,
                                      long idClient,
                                     out h_dailylogs DailyLog,
                                     out List<h_dailylog_involved_people> InvolvedPeople,
                                     out List<StaffCustomEntity> Staffs,
                                     out string clientName,
                                     out string clientImg,
                                     out List<ProjectCustomEntity> projects)
        {
            DailyLog = new h_dailylogs();
            Staffs = new List<StaffCustomEntity>();
            InvolvedPeople = new List<h_dailylog_involved_people>();
            clientName = "";
            clientImg = "";

            var isnew = idDailyLog < 0;

            //List<h_medical_remindersCustom> medicalRemindersAux = new List<h_medical_remindersCustom>();

            var clientAux = dataAccessLayer.GetClientById(idClient);

            projects = dataAccessLayer.GetProjectClientByIdPeriodIdClient(idPeriod, idClient).Select(x=>new
             ProjectCustomEntity
            {
                 Id = x.IdfProject,
                 Description = x.Description,
                 Color = x.Color                
            }).ToList();


            if (isnew)
            {
                // var idPeriod = dataAccessLayer.GetLastPeriod();

                if (idPeriod > 0)
                {
                    Staffs = dataAccessLayer.GetStaffForIncident(idPeriod).ToList();
                }

                DailyLog.Date = DateTime.Now;
                DailyLog.IdfPeriod = idPeriod;
                DailyLog.State = "C";
                DailyLog.ClientId = idClient;
                DailyLog.StaffOnShift = ".";

                // var cosa = dataAccessLayer.clien
                // DailyLog.ProjectId = clientAux.projects_clients.Single(c => c.IdfPeriod == idPeriod && c.State != "D").IdfProject;
                // DailyLog.ProjectId = dataAccessLayer.GetProjectClientByIdPeriodIdClient(idPeriod, idClient);
                // Injury.DateReportedSupervisor = DateTime.Now;
                // Injury.Id = -1;
                // Catalog.AddRange(cat);
                Staffs = dataAccessLayer.GetStaffForIncident(DailyLog.IdfPeriod).ToList();
            }
            else
            {
                DailyLog = dataAccessLayer.GetDailyLogById(idDailyLog);
                InvolvedPeople = dataAccessLayer.GetDailyLogInvolvedPeopleById(idDailyLog);
                Staffs = dataAccessLayer.GetStaffForIncident(DailyLog.IdfPeriod).ToList();
            }





            //clientName = string.Format("{0} {1}", clientAux.FullName)
            clientName = clientAux.FullName;
            clientImg = clientAux.Img;







            return true;
        }


        public bool SaveDailyLog(h_dailylogs DailyLog,
                                 List<h_dailylog_involved_people> InvolvedPeople,
                                 int timeDifference,
                                 out long iddailylog)
        {

            DailyLog.Date = DailyLog.Date.AddHours(timeDifference);

            

            dataAccessLayer.SaveDailyLogs(DailyLog, InvolvedPeople, out iddailylog);

            return true;
        }



    }
}