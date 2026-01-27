using System;
using System.Collections.Generic;

namespace JayGor.People.Entities
{
    public partial class identity_users
    {
        public identity_users()
        {
            identity_users_rol = new HashSet<identity_users_rol>();
            //identity_users_unidades = new HashSet<identity_users_unidades>();
        }

        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int? Eliminado { get; set; }

        public ICollection<identity_users_rol> identity_users_rol { get; set; }
        //public ICollection<identity_users_unidades> identity_users_unidades { get; set; }
    }
}
