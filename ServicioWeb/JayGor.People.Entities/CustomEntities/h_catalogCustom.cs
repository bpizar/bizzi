using System;
using JayGor.People.Entities.Entities;


namespace JayGor.People.Entities.CustomEntities
{
    public class h_catalogCustom : h_catalog
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public long IdValue { get; set; }
    }
}