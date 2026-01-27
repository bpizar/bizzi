using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllClientFormImageValues(out List<ClientFormImageValuesCustomEntity> clientFormImageValues)
        {
            clientFormImageValues = dataAccessLayer.GetAllClientFormImageValues().ToList();
        }

        public void GetClientFormImageValuebyId(long id,
                              out ClientFormImageValuesCustomEntity clientFormImageValueOut)
        {
            clientFormImageValueOut = new ClientFormImageValuesCustomEntity();

            if (id >= 0)
            {
                clientFormImageValueOut = dataAccessLayer.GetClientFormImageValuebyId(id);
            }
        }

        public async Task<CommonResponse> SaveClientFormImageValue(client_form_image_values clientFormImageValues)
        {
            return dataAccessLayer.SaveClientFormImageValue(clientFormImageValues);
        }

        public CommonResponse DeleteClientFormImageValue(long IdClientFormImageValue)
        {
            var result = dataAccessLayer.DeleteClientFormImageValues(IdClientFormImageValue);
            return result;
        }

        public void GetClientFormImageValueByClientFormAndClient(long idClientForm, long idClient, out ClientFormImageValuesCustomEntity clientFormImageValue)
        {
            clientFormImageValue = dataAccessLayer.GetClientFormImageValueByClientFormAndClient(idClientForm, idClient);
        }
    }
}