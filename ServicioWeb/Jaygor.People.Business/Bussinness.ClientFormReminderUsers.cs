using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllClientFormReminderUsers(out List<ClientFormReminderUsersCustomEntity> clientFormReminderUsers)
        {
            clientFormReminderUsers = dataAccessLayer.GetAllClientFormReminderUsers().ToList();
        }

        public void GetClientFormReminderUserbyId(long id,
                              out ClientFormReminderUsersCustomEntity clientFormReminderUserOut)
        {
            clientFormReminderUserOut = new ClientFormReminderUsersCustomEntity();

            if (id >= 0)
            {
                clientFormReminderUserOut = dataAccessLayer.GetClientFormReminderUserbyId(id);
            }
        }

        public CommonResponse SaveClientFormReminderUser(client_form_reminder_users clientFormReminderUsers)
        {
            var result = dataAccessLayer.SaveClientFormReminderUser(clientFormReminderUsers);
            return result;
        }

        public CommonResponse DeleteClientFormReminderUser(long IdClientFormReminderUser)
        {
            var result = dataAccessLayer.DeleteClientFormReminderUsers(IdClientFormReminderUser);
            return result;
        }

        public void GetAllClientFormReminderUsersByClientFormReminder(long idClientFormReminder, out List<ClientFormReminderUsersCustomEntity> clientFormReminderUsers)
        {
            clientFormReminderUsers = dataAccessLayer.GetAllClientFormReminderUsersByClientFormReminder(idClientFormReminder).ToList();
        }
    }
}