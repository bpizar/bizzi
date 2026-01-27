using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllStaffFormReminderUsers(out List<StaffFormReminderUsersCustomEntity> staffFormReminderUsers)
        {
            staffFormReminderUsers = dataAccessLayer.GetAllStaffFormReminderUsers().ToList();
        }

        public void GetStaffFormReminderUserbyId(long id,
                              out StaffFormReminderUsersCustomEntity staffFormReminderUserOut)
        {
            staffFormReminderUserOut = new StaffFormReminderUsersCustomEntity();

            if (id >= 0)
            {
                staffFormReminderUserOut = dataAccessLayer.GetStaffFormReminderUserbyId(id);
            }
        }

        public CommonResponse SaveStaffFormReminderUser(staff_form_reminder_users staffFormReminderUsers)
        {
            var result = dataAccessLayer.SaveStaffFormReminderUser(staffFormReminderUsers);
            return result;
        }

        public CommonResponse DeleteStaffFormReminderUser(long IdStaffFormReminderUser)
        {
            var result = dataAccessLayer.DeleteStaffFormReminderUsers(IdStaffFormReminderUser);
            return result;
        }

        public void GetAllStaffFormReminderUsersByStaffFormReminder(long idStaffFormReminder, out List<StaffFormReminderUsersCustomEntity> staffFormReminderUsers)
        {
            staffFormReminderUsers = dataAccessLayer.GetAllStaffFormReminderUsersByStaffFormReminder(idStaffFormReminder).ToList();
        }
    }
}