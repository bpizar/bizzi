using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllProjectFormReminderUsers(out List<ProjectFormReminderUsersCustomEntity> projectFormReminderUsers)
        {
            projectFormReminderUsers = dataAccessLayer.GetAllProjectFormReminderUsers().ToList();
        }

        public void GetProjectFormReminderUserbyId(long id,
                              out ProjectFormReminderUsersCustomEntity projectFormReminderUserOut)
        {
            projectFormReminderUserOut = new ProjectFormReminderUsersCustomEntity();

            if (id >= 0)
            {
                projectFormReminderUserOut = dataAccessLayer.GetProjectFormReminderUserbyId(id);
            }
        }

        public CommonResponse SaveProjectFormReminderUser(project_form_reminder_users projectFormReminderUsers)
        {
            var result = dataAccessLayer.SaveProjectFormReminderUser(projectFormReminderUsers);
            return result;
        }

        public CommonResponse DeleteProjectFormReminderUser(long IdProjectFormReminderUser)
        {
            var result = dataAccessLayer.DeleteProjectFormReminderUsers(IdProjectFormReminderUser);
            return result;
        }

        public void GetAllProjectFormReminderUsersByProjectFormReminder(long idProjectFormReminder, out List<ProjectFormReminderUsersCustomEntity> projectFormReminderUsers)
        {
            projectFormReminderUsers = dataAccessLayer.GetAllProjectFormReminderUsersByProjectFormReminder(idProjectFormReminder).ToList();
        }
    }
}