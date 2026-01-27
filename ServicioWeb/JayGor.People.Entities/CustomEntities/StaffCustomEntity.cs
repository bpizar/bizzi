using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Entities.CustomEntities
{
    public class StaffCustomEntity: staff
    {
        public string PositionName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Group { get; set; }
        public decimal Hours { get; set; }
        public long IdfPosition { get; set; }             
        public long IdfStaff { get; set; }          
        public string Abm { get; set; }   
        public string Img { get; set; }
        public int GeoTrackingEvery { get; set; }
        //public string group { get; set; }
        public string ProjectInfo { get; set; }
        public string ProjectColor { get; set; }
    }
}
