using Cybervision.IdentityServer.Models;

namespace Cybervision.IdentityServer.Services
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user);
        bool ValidateToken(string token);
    }
}
