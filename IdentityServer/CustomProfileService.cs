using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer
{
    public class CustomProfileService : ProfileService<IdentityUser>
    {
        public CustomProfileService(UserManager<IdentityUser> userManager, IUserClaimsPrincipalFactory<IdentityUser> claimsFactory) : base(userManager, claimsFactory)
        {
        }

        protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, IdentityUser user)
        {
            var principal = await GetUserClaimsAsync(user);
            var id = (ClaimsIdentity)principal.Identity!;
            if (!string.IsNullOrEmpty(user.UserName))
            {
                id.AddClaim(new Claim("Username", user.UserName));
            }

            context.AddRequestedClaims(principal.Claims);
        }
    }
}