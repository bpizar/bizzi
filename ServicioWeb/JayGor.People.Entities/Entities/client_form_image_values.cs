using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Entities
{
    public partial class client_form_image_values
    {
        public client_form_image_values()
        {
        }

        public long Id { get; set; }

        public long IdfClient { get; set; }
        
        public long IdfClientForm { get; set; }

        public string Image { get; set; }

        public DateTime FormDateTime { get; set; }

        public DateTime DateTime { get; set; }
    }
}
