using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace JayGor.People.Api.auth
{
    public class Tokens
    {
      public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory,string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
      {
        var response = new
        {
          id = identity.Claims.Single(c => c.Type == "id").Value,
          auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
          expires_in = (int)jwtOptions.ValidFor.TotalSeconds
        };

        return JsonConvert.SerializeObject(response, serializerSettings);
      }



    	public static async Task<string> GenerateJwtMobile(ClaimsIdentity identity, 
                                                           IJwtFactory jwtFactory, 
                                                           string userName, 
                                                           JwtIssuerOptions jwtOptions, 
                                                           JsonSerializerSettings serializerSettings,
                                                          // long userId,
                                                          string firstName,
                                                          string lastName,
                                                          string face,
                                                          bool isFaceRecorder,
                                                          int geoTrackingEvery,
                                                          string facestampin)
    	{
            var response = new
            {
                idx = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
                //UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Face = face,
                //Face = !string.IsNullOrEmpty(face)  ? Microsoft.AspNetCore.WebUtilities.WebEncoders.Base64UrlEncode(System.Text.Encoding.UTF8.GetBytes(face)) : face,
                //Face = System.Text.Encoding.UTF8.GetBytes(face),
                //Face = "12313123412341234123412341234123412341234123412341234",
                IsFaceRecorder = isFaceRecorder,
                GeoTrackingEvery = geoTrackingEvery,
                facestamp = facestampin
    		};

    		return JsonConvert.SerializeObject(response, serializerSettings);
    	}

    }
}