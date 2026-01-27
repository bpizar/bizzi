using System.Collections.Generic;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class AddStaffFormFieldRequest : CommonRequest
    {
        public long IdStaffForm { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Placeholder { get; set; }
        public string DataType { get; set; }
        public string Constraints { get; set; }
    }
}
