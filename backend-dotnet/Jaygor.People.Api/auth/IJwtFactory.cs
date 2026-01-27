using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JayGor.People.Api.auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id,List<Claim> rolesClaim);
    }
}