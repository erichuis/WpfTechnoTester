using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            [
                new IdentityResources.OpenId()
            ];

        public static IEnumerable<ApiScope> ApiScopes =>
            [
                new ApiScope(name: "TodoApi", displayName: "TodoApi")
            ];

        public static IEnumerable<Client> Clients =>
            [
                new Client
                {
                    ClientId = "WpfTodo",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                    AllowedScopes = {"https://localhost:5001/resources", "TodoApi" },
                    AccessTokenLifetime = 604800
                }
            ];
    }
}