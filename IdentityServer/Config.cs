using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name = "verification",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                }
            },
            new IdentityResource("color", new [] { "favorite_color" })

        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: "TodoApi", displayName: "TodoApi")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            //new Client
            //{
            //    ClientId = "WpfTodo",

            //    // no interactive user, use the clientid/secret for authentication
            //    AllowedGrantTypes = GrantTypes.ClientCredentials,

            //    // secret for authentication
            //    ClientSecrets =
            //    {
            //        new Secret("secret".Sha256())
            //    },

            //    // scopes that client has access to
            //     AllowedScopes = {"https://localhost:5001/resources", "TodoApi" },
            //},
            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "WpfTodo",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                
                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "verification",
                    "TodoApi",
                    "color"
                }
            }
        };
}

//using Duende.IdentityServer.Models;
//using Duende.IdentityServer.Test;
//using System.Security.Claims;

//namespace IdentityServer
//{
//    public static class Config
//    {
//        public static IEnumerable<IdentityResource> IdentityResources =>
//        new List<IdentityResource>
//        {
//            new IdentityResources.OpenId(),
//            new IdentityResources.Profile()
//        };

//        public static IEnumerable<ApiScope> ApiScopes =>
//            [
//                new ApiScope(name: "TodoApi", displayName: "TodoApi")
//            ];

//        public static IEnumerable<ApiResource> ApiResources =>
//        new List<ApiResource>
//        {
//            new ApiResource("TodoApi")
//            {
//                Scopes = { "TodoApi" },
//                UserClaims = { "name", "email" }
//            },
//            new ApiResource("api2")
//            {
//                Scopes = { "api2" },
//                UserClaims = { "name", "email" }
//            }
//        };
//        public static IEnumerable<Client> Clients =>
//        new List<Client>
//        {
//            new Client
//            {
//                ClientId = "WpfTodo",
//                AllowedGrantTypes = GrantTypes.Code,
//                RequirePkce = true,
//                RedirectUris = { "http://localhost/wpf/callback" },
//                PostLogoutRedirectUris = { "http://localhost/wpf/logout" },
//                AllowedScopes = { "openid", "profile", "TodoApi", "api2" },
//                AllowOfflineAccess = true
//            }
//        };
//        public static List<TestUser> Users =>
//        new List<TestUser>
//        {
//            new TestUser
//            {
//                SubjectId = "1",
//                Username = "testuser",
//                Password = "password",
//                Claims =
//                {
//                    new Claim("name", "Test User"),
//                    new Claim("email", "testuser@example.com")
//                }
//            }
//        };
//        //public static IEnumerable<Client> Clients =>
//        //    [
//        //        new Client
//        //        {
//        //            ClientId = "WpfTodo",
//        //            AllowedGrantTypes = GrantTypes.ClientCredentials,
//        //            ClientSecrets =
//        //                {
//        //                    new Secret("secret".Sha256())
//        //                },
//        //            AllowedScopes = {"https://localhost:5001/resources", "TodoApi" },
//        //            AccessTokenLifetime = 604800
//        //        }
//        //    ];
//    }
//}