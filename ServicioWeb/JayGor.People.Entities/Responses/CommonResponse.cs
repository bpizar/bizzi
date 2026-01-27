using System;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class CommonResponse
    {
        public bool Result { get; set; } = false;
        public List<GenericPair> Messages { get; set; } = new List<GenericPair>();          
        public string TagInfo { get; set; }

        public DateTime CurrentDateTime { get; set; } = DateTime.Now;
        public GenericPair Period { get; set; }
    }

    public class GenericPair {
        public string Id { get; set; }
        public string Description { get; set; }
    }

    public class GenericTriValue
    {
        public string Id { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
    }
}
