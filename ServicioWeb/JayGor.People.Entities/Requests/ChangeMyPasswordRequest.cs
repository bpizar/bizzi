using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class ChangeMyPasswordRequest : CommonRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}