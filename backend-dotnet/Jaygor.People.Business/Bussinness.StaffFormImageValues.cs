using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllStaffFormImageValues(out List<StaffFormImageValuesCustomEntity> staffFormImageValues)
        {
            staffFormImageValues = dataAccessLayer.GetAllStaffFormImageValues().ToList();
        }

        public void GetStaffFormImageValuebyId(long id,
                              out StaffFormImageValuesCustomEntity staffFormImageValueOut)
        {
            staffFormImageValueOut = new StaffFormImageValuesCustomEntity();

            if (id >= 0)
            {
                staffFormImageValueOut = dataAccessLayer.GetStaffFormImageValuebyId(id);
            }
        }

        public async Task<CommonResponse> SaveStaffFormImageValue(staff_form_image_values staffFormImageValue)
        {
            return dataAccessLayer.SaveStaffFormImageValue(staffFormImageValue);
        }

        public CommonResponse DeleteStaffFormImageValue(long IdStaffFormImageValue)
        {
            var result = dataAccessLayer.DeleteStaffFormImageValues(IdStaffFormImageValue);
            return result;
        }

        public void GetStaffFormImageValueByStaffFormAndStaff(long idStaffForm, long idStaff, out StaffFormImageValuesCustomEntity staffFormImageValue)
        {
            staffFormImageValue = dataAccessLayer.GetStaffFormImageValueByStaffFormAndStaff(idStaffForm, idStaff);
        }
    }
}