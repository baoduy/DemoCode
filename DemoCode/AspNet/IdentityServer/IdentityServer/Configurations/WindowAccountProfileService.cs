using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer.Configurations
{
    public class WindowAccountProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //if (context.Subject.Identity.IsAuthenticated 
            //    && context.Subject.Identity.Name?.Contains("Steven") == true)
            //    context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, "ApiRead"));
            context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, "ApiRead"));
            return Task.FromResult(true);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(true);
        }
    }
}
