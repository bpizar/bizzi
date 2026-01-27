using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public CommonResponse SaveStaff(staff staff,
                                        identity_users user,
                                        List<long> roles,
                                        string serverPath,
                                        string location, 
                                        string password,
                                        staff_period_settings staffPeriodSettings,
                                        int workingHoursByPeriodStaff,
                                        long idfperiod)
        {
            var isnew = staff.Id < 0;
            var result = dataAccessLayer.SaveStaff(staff, user, roles, password,workingHoursByPeriodStaff,idfperiod);


            if (!isnew && staffPeriodSettings!=null)
            {
                // var staffPeriodSettingsAux = dataAccessLayer.GetStaffPeriodSettings(staffPeriodSettings.IdfStaff, staffPeriodSettings.IdfPeriod); 

                //if (staffPeriodSettingsAux == null)
                //{
                dataAccessLayer.SaveStaffPeriodSettings(staffPeriodSettings.IdfStaff, idfperiod, staffPeriodSettings.WorkingHours);
                //dataAccessLayer.SaveStaffPeriodSettings(staffPeriodSettings.IdfStaff, staffPeriodSettings.IdfPeriod, staffPeriodSettings.WorkingHours);
                // staffPeriodSettings = dataAccessLayer.GetStaffPeriodSettings(staffPeriodSettings.IdfStaff, staffPeriodSettings.IdfPeriod);
                //}
            }

            //if(!isnew)
            //{
            //    dataAccessLayer.SaveStaffPeriodSettings(staffPeriodSettings.Id,);
            //}

            // if (result.Result && isnew)
            // {
                 // var pathOrigin = string.Format("{0}/{1}generic.png", serverPath, location);
                // var pathDestination = string.Format("{0}/{1}{2}.png", serverPath, location, result.TagInfo.Split('-')[0]);
                // var picture = System.IO.File.ReadAllBytes(pathOrigin);
                // System.IO.File.WriteAllBytes(pathDestination, picture);
            // }

            return result;
        }

        public bool ResetPassword(long id, string newPassword)
        {
            return dataAccessLayer.ResetPassword(id, newPassword);
        }

        public IEnumerable<StaffCustomEntity> GetStaffsForOwnerList(long idProject,long idPeriod,out List<ProjectOwnersCustom> owners)
        {
            owners = dataAccessLayer.GetProjectOwners(idProject,idPeriod).ToList();
            return dataAccessLayer.GetStaffsForOwnerList(idProject,idPeriod);
        }

        public IEnumerable<StaffCustomEntity> GetStaffs()
        {        
            var staffs = dataAccessLayer.GetStaffs();


            foreach(var st in staffs)
            {
                var AssignedProgramsToAux = string.Empty;
                var projectsAux = dataAccessLayer.GetProjectsByStaff(st.Id, dataAccessLayer.GetLastActivePeriod()); // or   st.Id ??

                var s = string.Empty;
                projectsAux.ToList().ForEach(c => s = string.Format("{0}" + (string.IsNullOrEmpty(s) ? "" : " , ") + "{1}", s, c.ProjectName ));
                AssignedProgramsToAux = s;

                st.ProjectInfo = s;
            }

            return staffs;

            // return dataAccessLayer.GetStaffs();
        }
      
        public void GetStaffbyId(long id,
                              out StaffCustomEntity staffOut,
                                 out List<Identity_RolesCustom> IdentityRoles)
        {
            staffOut = new StaffCustomEntity();
            IdentityRoles = new List<Identity_RolesCustom>();

            if (id >= 0)
            {
                staffOut = dataAccessLayer.GetStaffbyId(id);
                staffOut.IdfUserNavigation.Password = "*";
                staffOut.IdfUserNavigation.Face = "";

               

                //fix issue redundancia ciclica en objetos.
                IdentityRoles = dataAccessLayer.IdentityGetRoles(id).ToList();

                staffOut.IdfUserNavigation.staff = null;

            }
        }

        public IEnumerable<StaffForPlanningCustomEntity> GetStaffForPlanning(string groupby)
        {
            return dataAccessLayer.GetStaffForPlanning(groupby);
        }

        public void GetAllStaff(out List<StaffCustomEntity> staffs,out List<positions> positions )
        {
            staffs = dataAccessLayer.GetAllStaff().ToList();
            positions = dataAccessLayer.GetPositions().ToList();            
        }

        //public void GetStaffForScheduling(long idperiod, 
        //                                  out List<StaffForPlanningCustomEntity> Staffs,
        //                                  out List<OverTimeCustomEntity> Overtime)
        //{
        //    Staffs = dataAccessLayer.GetStaffForScheduling(idperiod).ToList();
        //    Staffs.ToList().ForEach(c=>c.HoursFree = c.Hours-c.HoursAssigned);

        //    Overtime = new List<OverTimeCustomEntity>();


        //    // return staffs;
        //}

        //public IEnumerable<StaffForPlanningCustomEntity> GetStaffByOwnProjects(long idperiod, string username)
        //{
        //    return dataAccessLayer.GetStaffByOwnProjects(idperiod, username);
        //}

        //public IEnumerable<StaffForPlanningCustomEntity> GetStaffByOwnScheduling(long idperiod, string username)
        //{
        //    return dataAccessLayer.GetStaffByOwnScheduling(idperiod, username);
        //}

        public CommonResponse EnableDisableAccount(long IdUser, string State)
        {
            return dataAccessLayer.EnableDisableAccount(IdUser, State);
        }





    }
}