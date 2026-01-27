using JayGor.People.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace JayGor.People.Entities.CustomEntities
{
    public class SchedulingCustomEntity : scheduling
    {
        public string AssignedToFullName { get; set; }
        public long IdUser { get; set; }
        public string AssignedToPosition { get; set; }
        public string ProjectName { get; set; }
        public string ProjectColor { get; set; }
        public long IdfStaff { get; set; }
        public string Abm { get; set; }
        public bool Draggable { get; set; } = true;
        public bool Resizable { get; set; } = true;
        public long Hours { get; set; } = 0;
        public string Img { get; set; }



        //New, erase if error.
        public string subject { get; set; }

        public bool IsDirty { get; set; }



    }
}
