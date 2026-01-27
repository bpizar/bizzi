using JayGor.People.Entities.CustomEntities;
using System.Collections.Generic;

namespace JayGor.People.Entities.Responses
{
    public class GetStaffsListResponse : CommonResponse
    {
        public List<StaffListCustomEntity> Staffs { get; set; } = new List<StaffListCustomEntity>();



        
    }
}