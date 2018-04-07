using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer.Configurations
{
    public static class Configs
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("testapi", "HBD Test API"){  UserClaims = {JwtClaimTypes.Role}}
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "TestClientCredentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // secret for authentication
                    ClientSecrets ={new Secret("secret".Sha256())},
                    // scopes that client has access to
                    AllowedScopes = { "testapi" }
                },
                new Client
                {
                    ClientId = "TestResourceOwnerPassword",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    // secret for authentication
                    ClientSecrets ={new Secret("secret".Sha256())},
                    // scopes that client has access to
                    AllowedScopes = { "testapi" }
                },
                new Client
                {
                    ClientId = "TestMvc",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:1111/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:1111/signout-callback-oidc" },
                    // secret for authentication
                    ClientSecrets ={new Secret("secret".Sha256())},
                    // scopes that client has access to
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                },
                new Client
                {
                    ClientId = "TestMvcHybridAndClientCredentials",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:1111/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:1111/signout-callback-oidc" },
                    // secret for authentication
                    ClientSecrets ={new Secret("secret".Sha256())},
                    // scopes that client has access to
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "testapi"
                    },
                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "test_react",
                    ClientName = "React Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    AlwaysIncludeUserClaimsInIdToken=true,
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess=true,
                    AccessTokenType=AccessTokenType.Jwt,

                    RedirectUris =           { "http://localhost:1112/callback" },
                    PostLogoutRedirectUris = { "http://localhost:1112" },
                    AllowedCorsOrigins =     { "http://localhost:1112" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "testapi"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "duy",
                    Password = "duy",
                    Claims = new []
                    {
                        new Claim("name", "Hoang Bao Duy"),
                        new Claim("website", "https://drunkcoding.net"),
                        new Claim(JwtClaimTypes.Role, "ApiRead")
                    }
                },
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("role",new[]{JwtClaimTypes.Role })
            };
        }
    }
}
