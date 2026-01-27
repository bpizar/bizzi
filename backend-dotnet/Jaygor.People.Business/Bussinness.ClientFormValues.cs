using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllClientFormValues(out List<ClientFormValuesCustomEntity> clientFormValues)
        {
            clientFormValues = dataAccessLayer.GetAllClientFormValues().ToList();
        }

        public void GetClientFormValuebyId(long id,
                              out ClientFormValuesCustomEntity clientFormValueOut)
        {
            clientFormValueOut = new ClientFormValuesCustomEntity();

            if (id >= 0)
            {
                clientFormValueOut = dataAccessLayer.GetClientFormValuebyId(id);
            }
        }

        public CommonResponse SaveClientFormValue(client_form_values clientFormValues)
        {
            var result = dataAccessLayer.SaveClientFormValue(clientFormValues);
            return result;
        }

        public CommonResponse DeleteClientFormValue(long IdClientFormValue)
        {
            var result = dataAccessLayer.DeleteClientFormValues(IdClientFormValue);
            return result;
        }

        public void GetAllClientFormValuesByClientFormAndClient(long idClientForm, long idClient, out List<ClientFormValuesCustomEntity> clientFormValues)
        {
            clientFormValues = dataAccessLayer.GetAllClientFormValuesByClientFormAndClient(idClientForm, idClient).ToList();
        }

        public CommonResponse SaveClientFormValueWithDetail(client_form_values clientFormValues,client_form_field_values[] ClientFormFieldValues)
        {
            var result = dataAccessLayer.SaveClientFormValueWithDetail(clientFormValues, ClientFormFieldValues);
            return result;
        }
    }
}