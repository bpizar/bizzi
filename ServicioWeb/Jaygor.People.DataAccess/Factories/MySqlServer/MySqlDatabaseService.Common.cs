using System;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public string Ping()
        {
            return string.Format("Pong - {0}", DateTime.Now);
        }

        public CommonResponse CommonSaveError(string errorDescription, long idfuser = 1)
        {
            var response = new CommonResponse();
                       
            try
            {
                var errorCall = new common_errors { Description = errorDescription, IdfUser = idfuser };
                context.common_errors.Add(errorCall);
                context.SaveChanges();
                response.Result = true;
                response.TagInfo = errorCall.Id.ToString();             
            }
            catch (Exception)
            {
                ;
            }

            return response;
        }
    }
}








//private void EstablishPermissions(MySqlContextDB context, long idParticipant, long idProject, long idfPosition)
//{
//    var idPosition = context.staff_project_position.Where(c => c.IdfStaff == idParticipant && c.IdfProject == idProject && c.State != "D" && c.IdfPosition == idfPosition).Single().IdfPosition;
//    var roles = context.ro .positions_roles.Where(c => c.IdfPosition == idPosition && c.State != "D").ToList();

//    var user = context.Staff_Project_Position
//                      .Include(d=>d.STAFF)
//                      .ThenInclude(d=>d.USER)
//                      .Where(c => c.IdfStaff == idParticipant && c.IdfProject == idProject && c.IdfPosition == idfPosition && c.State!="D")
//                      .Single()
//                      .STAFF.USER;

//    foreach (var r in roles)
//    {
//        var findrole = context.Identity_Users_Rol.Where(c => c.IdfRol == r.IdfRol && c.IdfUser== user.Id && c.State != "D").ToList();
//        if (findrole.Count == 0)
//        {
//            context.Identity_Users_Rol.Add(new Identity_Users_Rol { IdfRol = r.IdfRol, IdfUser = user.Id, State = "C" });
//        }
//    }
//}


//private void EstablishPermissionsByPositions(MySqlServerContextDB context, long idRol, long idPosition)
//{
//    var ls = context.Staff_Project_Position.Include(i => i.STAFF).ThenInclude(d => d.USER).ThenInclude(c => c.IDENTITYUSERROLES).Where(c => c.IdfPosition == idPosition && c.State != "D");

//    foreach (var s in ls)
//    {
//        var hasRol = s.STAFF.USER.IDENTITYUSERROLES.Where(c => c.IdfRol == idRol && c.State != "D").Count() > 0;

//        if (!hasRol)
//        {
//            var addrol = new Identity_Users_Rol
//            {
//                IdfRol = idRol,
//                IdfUser = s.STAFF.IdfUser,
//                State = "C"
//            };

//            context.Identity_Users_Rol.Add(addrol);
//        }
//    }
//}

//private void RemovePermissionsByPositions(MySqlServerContextDB context, long idRol, long idPosition)
//{

//    foreach (var spp in context.Staff_Project_Position.Include(c => c.STAFF).ThenInclude(c => c.USER).ThenInclude(c => c.IDENTITYUSERROLES).Where(c => c.IdfPosition == idPosition && c.State != "D"))
//    {
//        var idUser = spp.STAFF.IdfUser;
//        spp.STAFF.USER.IDENTITYUSERROLES.Where(c => c.IdfUser == idUser && c.IdfRol == idRol && c.State != "D" && c.IdfRol != this.IdRolUser).ToList().ForEach(c => c.State = "D");
//    }
//}


//private void RemovePermissions(MySqlServerContextDB context, long idParticipant, long idProject, long idfPosition)
//{
//    var spp = context.Staff_Project_Position
//                     .Include(c => c.STAFF).ThenInclude(c => c.USER).ThenInclude(c => c.IDENTITYUSERROLES)   
//                     .Where(c => c.IdfStaff == idParticipant && c.State != "D")
//                     .ToList();

//    var idPositionCurrent = context.Staff_Project_Position.Where(c => c.IdfStaff == idParticipant && c.IdfProject == idProject && c.State != "D" && c.IdfPosition == idfPosition).Single().IdfPosition;
//    var rolesCurrent = context.Positions_Roles.Where(c => c.IdfPosition == idPositionCurrent && c.State != "D").ToList();

//    if (spp.Count == 1)
//    {
//        foreach (var r in rolesCurrent)
//        {
//            spp.Single().STAFF.USER.IDENTITYUSERROLES.Where(c => c.IdfRol != this.IdRolUser && c.IdfRol == r.IdfRol & c.State != "D").ToList().ForEach(c => c.State = "D");
//        }
//    }
//    else {
//        foreach (var cr in rolesCurrent) 
//        {
//            var roleExists = false;                    
//            foreach (var s in spp.Where(c => c.IdfProject != idProject))
//            {
//                var roles = context.Positions_Roles.Where(c => c.IdfRol != this.IdRolUser && c.IdfPosition == s.IdfPosition && c.State != "D").ToList();
//                foreach (var r in roles)
//                {
//                    if (cr.IdfRol == r.IdfRol)
//                    {
//                        roleExists = true;
//                        break;
//                    }
//                }

//            }

//            if (!roleExists)
//            {
//                var user = context.Staff_Project_Position
//                .Include(d => d.STAFF)
//                .ThenInclude(d => d.USER)
//                .Where(c => c.IdfStaff == idParticipant && c.IdfProject == idProject && c.IdfPosition == idfPosition && c.State != "D")
//                .Single()
//                .STAFF.USER;

//                var identityUserRol = context.Identity_Users_Rol.Where(c => c.IdfRol != this.IdRolUser && c.IdfRol == cr.IdfRol && c.IdfUser == user.Id && c.State != "D");

//                if (identityUserRol != null)
//                {

//                    identityUserRol.Single().State = "D";
//                }
//                else {
//                    //create?
//                }
//            }
//        }
//    }
//}