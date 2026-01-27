using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class EnableDisableAccountRequest : CommonRequest
    {
        public long IdUser { get; set; }
        public string State { get; set; }
    }
}
