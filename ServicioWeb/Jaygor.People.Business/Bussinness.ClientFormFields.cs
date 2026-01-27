using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;
using JayGor.People.Entities.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {
        public void GetAllClientFormFields(out List<ClientFormFieldsCustomEntity> clientFormFields)
        {
            clientFormFields = dataAccessLayer.GetAllClientFormFields().ToList();
        }

        public void GetClientFormFieldbyId(long id,
                              out ClientFormFieldsCustomEntity clientFormFieldsOut)
        {
            clientFormFieldsOut = new ClientFormFieldsCustomEntity();

            if (id >= 0)
            {
                clientFormFieldsOut = dataAccessLayer.GetClientFormFieldbyId(id);
            }
        }

        public CommonResponse SaveClientFormField(client_form_fields clientFormFields)
        {
            var result = dataAccessLayer.SaveClientFormField(clientFormFields);
            return result;
        }

        public CommonResponse DeleteClientFormField(long IdClientFormField)
        {
            var result = dataAccessLayer.DeleteClientFormFields(IdClientFormField);
            return result;
        }

        public CommonResponse RemoveClientFormField(long IdClientForm, long IdFormField)
        {
            var result = dataAccessLayer.RemoveClientFormField(IdClientForm, IdFormField);
            return result;
        }

        public CommonResponse AddClientFormField(long idClientForm, string name, string description, string placeholder, string dataType, string constraints)
        {
            var result = dataAccessLayer.AddClientFormField(idClientForm, name, description, placeholder, dataType, constraints);
            return result;
        }
    }
}