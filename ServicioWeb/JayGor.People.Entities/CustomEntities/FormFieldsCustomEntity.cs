using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class FormFieldsCustomEntity : form_fields
    {
        public int Position { get; set; }
        public bool IsEnabled { get; set; }
    }
}
