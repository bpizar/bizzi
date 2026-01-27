using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllClientFormReminders(out List<ClientFormRemindersCustomEntity> clientFormReminders)
        {
            clientFormReminders = dataAccessLayer.GetAllClientFormReminders().ToList();
        }

        public void GetClientFormReminderbyId(long id,
                              out ClientFormRemindersCustomEntity clientFormReminderOut)
        {
            clientFormReminderOut = new ClientFormRemindersCustomEntity();

            if (id >= 0)
            {
                clientFormReminderOut = dataAccessLayer.GetClientFormReminderbyId(id);
            }
        }

        public CommonResponse SaveClientFormReminder(client_form_reminders clientFormReminders)
        {
            var result = dataAccessLayer.SaveClientFormReminder(clientFormReminders);
            return result;
        }

        public CommonResponse DeleteClientFormReminder(long IdClientFormReminder)
        {
            var result = dataAccessLayer.DeleteClientFormReminders(IdClientFormReminder);
            return result;
        }

        public void GetAllClientFormRemindersByClientForm(long idClientForm, out List<ClientFormRemindersCustomEntity> clientFormReminders)
        {
            clientFormReminders = dataAccessLayer.GetAllClientFormRemindersByClientForm(idClientForm).ToList();
        }
    }
}