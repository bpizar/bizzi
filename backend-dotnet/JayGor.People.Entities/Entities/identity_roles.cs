using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class identity_roles
    {
        public identity_roles()
        {
            identity_users_rol = new HashSet<identity_users_rol>();
        }

        public long Id { get; set; }
        public string Rol { get; set; }
        public string State { get; set; }
        public string DisplayShortName { get; set; }
        public string RolDescription { get; set; }

        public virtual ICollection<identity_users_rol> identity_users_rol { get; set; }
    }
}
