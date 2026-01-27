using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public IdentityCustomentity GetMyAccount(string username)
        {
            return dataAccessLayer.GetMyAccount(username);
        }

        public CommonResponse ChangeMyPassword(string username,
                                               string CurrentPassword,
                                               string NewPassword,
                                               string ConfirmNewPassword)        
        {
            return dataAccessLayer.ChangeMyPassword(username, CurrentPassword, NewPassword, ConfirmNewPassword);
        }

        public CommonResponse IdentityCreateUser(string email, string password)
        {
            return dataAccessLayer.IdentityCreateUser(email, password);
        }

        public identity_users IdentityGetUserByCredentials(string email, string password)
        {
            var aux = dataAccessLayer.IdentityGetUserByCredentials(email, password);
            if (aux is null)
                return aux;
            var toremove = new List<identity_users_rol>(); //;

            //foreach(var tr in aux.identity_users_rol.Where(c => c.State != "A"))
            //{
            toremove.AddRange(aux.identity_users_rol.Where(c => c.State != "A").ToList());
            //}


            foreach(var r in toremove)
            {
                aux.identity_users_rol.Remove(r);     
            }

            return aux;    
        }

        public identity_users IdentityGetUserByEmail(string mail)
        {
            return dataAccessLayer.IdentityGetUserByEmail(mail);
        }

        public identity_users IdentityGetUserById(long id)
        {
            return dataAccessLayer.IdentityGetUserById(id);
        }

        //public IEnumerable<Identity_Roles> IdentityGetRoles()
        //{
        //    return dataAccessLayer.IdentityGetRoles();
        //}


        public bool IdentityUpdateIdOneSignal(long id,string idOneSignal)
        {
            return dataAccessLayer.IdentityUpdateIdOneSignal(id, idOneSignal);
        }

        public bool IdentityUpdateIdOneSignalBrowser(long id, string idOneSignal)
        {
            return dataAccessLayer.IdentityUpdateIdOneSignaWeb(id, idOneSignal);
        }

        public bool IdentityUpdateTFASecret(long id, string TFASecret)
        {
            return dataAccessLayer.IdentityUpdateTFASecret(id, TFASecret);
        }
    }
}
