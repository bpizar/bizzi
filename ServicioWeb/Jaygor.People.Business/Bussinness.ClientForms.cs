using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllClientForms(out List<ClientFormsCustomEntity> clientForms)
        {
            clientForms = dataAccessLayer.GetAllClientForms().ToList();
        }

        public void GetClientFormbyId(long id,
                              out ClientFormsCustomEntity clientFormsOut)
        {
            clientFormsOut = new ClientFormsCustomEntity();

            if (id >= 0)
            {
                clientFormsOut = dataAccessLayer.GetClientFormbyId(id);
            }
        }

        public CommonResponse SaveClientForm(client_forms clientForms)
        {
            var result = dataAccessLayer.SaveClientForm(clientForms);
            return result;
        }

        public CommonResponse DeleteClientForm(long IdClientForm)
        {
            var result = dataAccessLayer.DeleteClientForms(IdClientForm);
            return result;
        }

        public void GetAllClientFormByClients(long IdClient, out List<ClientFormbyClientCustomEntity> clientFormbyClientCustomEntity)
        {
            clientFormbyClientCustomEntity = dataAccessLayer.GetAllClientFormsByClient(IdClient).ToList();
        }

        public void GetAllClientFormsByClientandClientForm(long IdClient, long IdClientForm, out List<ClientFormbyClientCustomEntity> clientFormbyClientCustomEntity)
        {
            clientFormbyClientCustomEntity = dataAccessLayer.GetAllClientFormsByClientandClientForm(IdClient, IdClientForm).ToList();
        }

        public CommonResponse SaveClientFormWithReminders(client_forms clientForm, client_form_reminders[] ClientFormReminders, FormFieldsCustomEntity[] FormFields)
        {
            var result = dataAccessLayer.SaveClientFormWithReminders(clientForm, ClientFormReminders, FormFields);
            return result;
        }
    }
}