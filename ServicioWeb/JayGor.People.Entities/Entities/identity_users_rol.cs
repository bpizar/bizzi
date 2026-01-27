using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class identity_users_rol
    {
        public long Id { get; set; }
        public long IdfUser { get; set; }
        public long IdfRol { get; set; }
        public string State { get; set; }

        public virtual identity_roles IdfRolNavigation { get; set; }
        public virtual identity_users IdfUserNavigation { get; set; }
    }
}
