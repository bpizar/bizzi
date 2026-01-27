using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllProjectFormReminders(out List<ProjectFormRemindersCustomEntity> projectFormReminders)
        {
            projectFormReminders = dataAccessLayer.GetAllProjectFormReminders().ToList();
        }

        public void GetProjectFormReminderbyId(long id,
                              out ProjectFormRemindersCustomEntity projectFormReminderOut)
        {
            projectFormReminderOut = new ProjectFormRemindersCustomEntity();

            if (id >= 0)
            {
                projectFormReminderOut = dataAccessLayer.GetProjectFormReminderbyId(id);
            }
        }

        public CommonResponse SaveProjectFormReminder(project_form_reminders projectFormReminders)
        {
            var result = dataAccessLayer.SaveProjectFormReminder(projectFormReminders);
            return result;
        }

        public CommonResponse DeleteProjectFormReminder(long IdProjectFormReminder)
        {
            var result = dataAccessLayer.DeleteProjectFormReminders(IdProjectFormReminder);
            return result;
        }

        public void GetAllProjectFormRemindersByProjectForm(long idProjectForm, out List<ProjectFormRemindersCustomEntity> projectFormReminders)
        {
            projectFormReminders = dataAccessLayer.GetAllProjectFormRemindersByProjectForm(idProjectForm).ToList();
        }
    }
}