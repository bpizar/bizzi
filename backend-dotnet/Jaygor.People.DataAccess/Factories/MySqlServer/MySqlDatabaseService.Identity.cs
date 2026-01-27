using System;
using System.Collections.Generic;
using System.Linq;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using Microsoft.EntityFrameworkCore;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public CommonResponse ChangeMyPassword(string username,
                                       string CurrentPassword,
                                       string NewPassword,
                                       string ConfirmNewPassword)
        {
            Exception error = null;
            var result = new CommonResponse();

           
            var user = context.identity_users.Where(c => c.Email == username && c.Password == CurrentPassword).FirstOrDefault();

            if (user == null)
            {
                result.Messages.Add(new GenericPair { Id = "2000", Description = "The current password does not match" });
                result.Result = false;
            }
            else {
                user.Password = NewPassword;
                context.SaveChanges();
                result.Result = true;
            }

            return result;        
        }

        public IdentityCustomentity GetMyAccount(string username)
        {
			Exception error = null;
			var result = new IdentityCustomentity();
            		
            result = context.identity_users
                            .Include(c=>c.identity_users_rol)
                            .Select(s => new IdentityCustomentity
                            {
                                Id = s.Id,
                                Email = s.Email,
                                FirstName = s.FirstName,
                                LastName = s.LastName,
                                Password = "*",
                                State = s.State,
                                identity_users_rol =  context.identity_users_rol.Where(c=>c.IdfUser == s.Id && c.State !="D").ToList(),
                                Img = s.identity_images.Where(c => c.Id == s.IdfImg).Single().Name
                            })
                            .Where(c => c.Email == username).FirstOrDefault();

            result.identity_users_rol.ToList().ForEach(c => c.IdfRolNavigation = context.identity_roles.Where(d => d.Id == c.IdfRol).Single());
			   
			return result;			
        }

        public CommonResponse IdentityCreateUser(string email, string password)
        {
			var response = new CommonResponse();                   
            context.identity_users.Add(new identity_users { Email = email, Password = password, State = "C" });
            context.SaveChanges();
            response.Result = true;
			return response;
        }

        public identity_users IdentityGetUserByCredentials(string email, string password)
        {
            return context.identity_users
                            .Include(t => t.identity_users_rol).ThenInclude(t => t.IdfRolNavigation)
                            .Where(c => c.Email == email && c.Password == password && c.staff.First().State!="D") // esto es nuevo && c.staff.First().State!="D")
					.FirstOrDefault();                
        }

        public identity_users IdentityGetUserById(long id)
        {
                var idUserAux = context.staff.Where(c => c.Id == id).FirstOrDefault().IdfUser;
                return context.identity_users.Where(c => c.Id == idUserAux).FirstOrDefault();
        }

        public identity_users IdentityGetUserByEmail(string mail)
        {
			return  context.identity_users.Where(c => c.Email == mail).FirstOrDefault();			
        }

        //private bool ContainsRole(long IdRolSearch, List<identity_users_rol> roles)
        //{
        //    foreach (var x in roles)
        //    {
        //        if (x.IdfRol == IdRolSearch && x.State == "A")
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        private bool ContainsRole(long IdRolSearch, List<long> roles)
        {
            foreach (var x in roles)
            {
                //if (x == IdRolSearch && x.State == "A")
                if (x == IdRolSearch)
                {
                    return true;
                }
            }

            return false;
        }


        public IEnumerable<Identity_RolesCustom> IdentityGetRoles(long IdStaff)
        {
			var result = new List<Identity_RolesCustom>();


            //var rolesUser = context.staff
            //                       .Include(cw => cw.IdfUserNavigation) .ThenInclude(c => c.identity_users_rol)
            //                   .Where(c => c.Id == IdStaff)
            //                   .Single()
            //                       .IdfUserNavigation.identity_users_rol;


            //var rolesUser = context.staff.Where(c => c.Id == IdStaff).Single().IdfUserNavigation.identity_users_rol.ToList();


            var rolesUserAux = context.identity_roles.Where(c => c.identity_users_rol.Where(d=>d.State != "D" && d.IdfUserNavigation.staff.Where(r=>r.Id == IdStaff).Any()).Any()).ToList(); // .Where(c => c.IdfUserNavigation.staff.First().Id == IdStaff && c.State != "D").ToList();

           

            var rolesUser = new List<long>();

            rolesUserAux.ForEach(c => rolesUser.Add(c.Id));

            //foreach(var )
            // context.ChangeTracker.A  .AcceptAllChanges();




            var roles = context.identity_roles.Where(c => c.State != "D");




            foreach (var r in roles)
	        {
	            var newRecord = new Identity_RolesCustom()
	            {
	                Id = r.Id,
	                DisplayShortName = r.DisplayShortName,
	                RolDescription = r.RolDescription,
	                Abm = "",
	                Group = this.ContainsRole(r.Id, rolesUser) ? "Assigned" : "Not Assigned",
	                IsInrole = this.ContainsRole(r.Id, rolesUser)
	            };

	            result.Add(newRecord);
	        }
			  
			return result;
        }


        public bool UpdateIdentityImage(long id, string fileName)
        {
	        var newRecord = new identity_images
	        {
	            Name = fileName,
                IdfIdentity_user = id
	        };

	        context.identity_images.Add(newRecord);
	        context.SaveChanges();

            context.identity_users.Where(c => c.Id == id).Single().IdfImg = newRecord.Id;
	        context.SaveChanges();
            		
            return true;
        }


        public bool IdentityUpdateIdOneSignal(long id, string idOneSignal)
        {
            context.identity_users.Where(c => c.Id == id).Single().IdOneSignal = idOneSignal;
            context.SaveChanges();
            return true;
        }

        public bool IdentityUpdateIdOneSignaWeb(long id, string idOneSignal)
        {
            context.identity_users.Where(c => c.Id == id).Single().IdOneSignalWeb = idOneSignal;
            context.SaveChanges();
            return true;
        }

        public bool IdentityUpdateTFASecret(long id, string TFASecret)
        {
            context.identity_users.Where(c => c.Id == id).Single().TFASecret = TFASecret;
            context.SaveChanges();
            return true;
        }
    }
}