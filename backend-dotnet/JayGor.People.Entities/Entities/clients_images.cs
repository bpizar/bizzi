using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class clients_images
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long IdfClient { get; set; }

        public virtual clients IdfClientNavigation { get; set; }
    }
}
