using System;
using System.Collections.Generic;

namespace JayGor.People.Entities
{
    public partial class identity_users_rol
    {
        public long Id { get; set; }
        public long IdfRol { get; set; }
        public long IdfUser { get; set; }
        public string State { get; set; }
        public int? Eliminado { get; set; }

        public identity_roles IdfRolNavigation { get; set; }
        public identity_users IdfUserNavigation { get; set; }
    }
}
