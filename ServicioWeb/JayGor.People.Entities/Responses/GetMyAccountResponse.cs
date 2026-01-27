using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Entities;

namespace JayGor.People.Entities.Responses
{
    public class GetMyAccountResponse : CommonResponse
    {
        public IdentityCustomentity User { get; set; } = new IdentityCustomentity();
    }
}
