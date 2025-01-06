using Microsoft.AspNetCore.Identity;

namespace Cybervision.IdentityServer.Models
{
    public class ApplicationUser : IdentityUser
    {
        string Username { get; set; }
    }
}
