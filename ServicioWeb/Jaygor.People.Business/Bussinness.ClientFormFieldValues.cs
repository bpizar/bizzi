using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllClientFormFieldValues(out List<ClientFormFieldValuesCustomEntity> clientFormFieldValues)
        {
            clientFormFieldValues = dataAccessLayer.GetAllClientFormFieldValues().ToList();
        }

        public void GetClientFormFieldValuebyId(long id,
                              out ClientFormFieldValuesCustomEntity clientFormFieldValuesOut)
        {
            clientFormFieldValuesOut = new ClientFormFieldValuesCustomEntity();

            if (id >= 0)
            {
                clientFormFieldValuesOut = dataAccessLayer.GetClientFormFieldValuebyId(id);
            }
        }

        public CommonResponse SaveClientFormFieldValue(client_form_field_values clientFormFieldValues)
        {
            var result = dataAccessLayer.SaveClientFormFieldValue(clientFormFieldValues);
            return result;
        }

        public CommonResponse DeleteClientFormFieldValue(long IdClientFormFieldValue)
        {
            var result = dataAccessLayer.DeleteClientFormFieldValues(IdClientFormFieldValue);
            return result;
        }

        public void GetAllClientFormFieldValuesByClientFormAndClient(long idClientForm, long idClient, out List<ClientFormFieldValuesCustomEntity> clientFormFieldValues)
        {
            clientFormFieldValues = dataAccessLayer.GetAllClientFormFieldValuesByClientFormAndClient(idClientForm, idClient).ToList();
        }

        public void GetAllClientFormFieldValuesByClientFormValue(long idClientFormValue, out List<ClientFormFieldValuesCustomEntity> clientFormFieldValues)
        {
            clientFormFieldValues = dataAccessLayer.GetAllClientFormFieldValuesByClientFormValue(idClientFormValue).ToList();
        }
    }
}