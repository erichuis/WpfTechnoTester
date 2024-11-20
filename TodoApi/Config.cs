
//using System.Collections.Generic;

//public static class Config
//{
//    // 1. Define API Scopes
//    public static IEnumerable<ApiScope> GetApiScopes() =>
//        new List<ApiScope>
//        {
//            new("api1", "My API")  // Define API scope with a name and display name
//        };

//    // 2. Define Identity Resources (OpenID, profile)
//    public static IEnumerable<IdentityResource> GetIdentityResources() =>
//        new List<IdentityResource>
//        {
//            new IdentityResources.OpenId(),
//            new IdentityResources.Profile()
//        };

//    // 3. Define Clients
//    public static IEnumerable<Client> GetClients() =>
//        new List<Client>
//        {
//            new Client
//            {
//                ClientId = "client_id",               // Unique identifier for the client
//                AllowedGrantTypes = GrantTypes.ClientCredentials,
//ClientSecrets = { new Secret("client_secret".Sha256()) },  // Hash the client secret
//                AllowedScopes = { "api1" }            // Scope this client is allowed to access
//            },
//            new Client
//            {
//                ClientId = "interactive_client",      // Another client example with different grant type
//                AllowedGrantTypes = GrantTypes.Code,
//                RequirePkce = true,
//                RedirectUris = { "https://localhost:5002/signin-oidc" },
//                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
//                ClientSecrets = { new Secret("interactive_secret".Sha256()) },
//                AllowedScopes = { "openid", "profile", "api1" }  // Include identity and API scopes
//            }
//        };

//// 4. Define Test Users (optional)
////public static List<TestUser> GetUsers() =>
////    new List<TestUser>
////    {
////            new TestUser
////            {
////                SubjectId = "1",
////                Username = "alice",
////                Password = "password"
////            },
////            new TestUser
////            {
////                SubjectId = "2",
////                Username = "bob",
////                Password = "password"
////            }
////    };
//}
