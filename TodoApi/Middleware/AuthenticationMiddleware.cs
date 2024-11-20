using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace TodoApi.Middleware
{
    public static class AuthenticationMiddleware
    {
        public static void UseCustomAuthenticationMiddleware(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();



            // Add endpoints
            app.MapGet("/secure", () => Results.Unauthorized()); // Simulates a 401 Unauthorized
            app.MapGet("/notfound", () => Results.NotFound());   // Simulates a 404 Not Found

            app.MapGet("identity", (ClaimsPrincipal user) =>
                user.Claims.Select(c => new { c.Type, c.Value }))
                .RequireAuthorization("ApiScope");

        }
    }

}
