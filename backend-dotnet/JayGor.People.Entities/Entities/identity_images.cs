using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class identity_images
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long IdfIdentity_user { get; set; }

        public virtual identity_users IdfIdentity_userNavigation { get; set; }
    }
}
