using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllStaffFormReminders(out List<StaffFormRemindersCustomEntity> staffFormReminders)
        {
            staffFormReminders = dataAccessLayer.GetAllStaffFormReminders().ToList();
        }

        public void GetStaffFormReminderbyId(long id,
                              out StaffFormRemindersCustomEntity staffFormReminderOut)
        {
            staffFormReminderOut = new StaffFormRemindersCustomEntity();

            if (id >= 0)
            {
                staffFormReminderOut = dataAccessLayer.GetStaffFormReminderbyId(id);
            }
        }

        public CommonResponse SaveStaffFormReminder(staff_form_reminders staffFormReminders)
        {
            var result = dataAccessLayer.SaveStaffFormReminder(staffFormReminders);
            return result;
        }

        public CommonResponse DeleteStaffFormReminder(long IdStaffFormReminder)
        {
            var result = dataAccessLayer.DeleteStaffFormReminders(IdStaffFormReminder);
            return result;
        }

        public void GetAllStaffFormRemindersByStaffForm(long idStaffForm, out List<StaffFormRemindersCustomEntity> staffFormReminders)
        {
            staffFormReminders = dataAccessLayer.GetAllStaffFormRemindersByStaffForm(idStaffForm).ToList();
        }
    }
}