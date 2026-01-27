using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class StaffListCustomEntity
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string PositionName { get; set; }
        public string Email { get; set; }
        public long IdfUser { get; set; }
        public string Img { get; set; }
        public string CellNumber { get; set; }
        public string HomePhone { get; set; }
        public string City { get; set; }
        public string ProjectInfo { get; set; }
    }
}
