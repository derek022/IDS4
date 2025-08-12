using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Shared;

namespace IDS4;

public static class Config
{
    public static IEnumerable<Client> Clients = new[]
    {
        new Client()
        {
            ClientId = "sample_client",
            ClientSecrets ={ new Secret("sample_client_secret".Sha256())},
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = {"sample_api"}
        },
        new Client()
        {
            ClientId = "sample_pass_client",
            ClientSecrets ={ new Secret("sample_client_secret".Sha256())},
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowedScopes = {"sample_api"}
        },
        new Client()
        {
            ProtocolType = "oidc",
            ClientId = "sample_mvc_client",
            ClientName = "Sample MVC Client",
            ClientSecrets = {new Secret("sample_client_secret".Sha256())},
            AllowedGrantTypes = GrantTypes.Hybrid,
            RedirectUris = {UrlOptions.WwwUrl + "signin-oidc"},
            PostLogoutRedirectUris = {UrlOptions.WwwUrl + "signout-callback-oidc"},
            AllowedCorsOrigins = {UrlOptions.WwwUrl},
            AllowedScopes = new List<string>()
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "sample_api"
            },
            RequireConsent = true,
            AlwaysIncludeUserClaimsInIdToken = true,
            RequirePkce = false,
            AccessTokenLifetime = 31536000,
            IdentityTokenLifetime = 300
        }
    };

    public static IEnumerable<ApiScope> ApiScopes = new[]
    {
        new ApiScope()
        {
            Name = "sample_api",
            Description = "Sample API"
        },
        new ApiScope()
    };

    public static IEnumerable<IdentityResource> IdentityResources = new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };

    public static List<TestUser> Users = new List<TestUser>()
    {
        new TestUser()
        {
            SubjectId = "1",
            Username = "admin",
            Password = "password"
        }
    };
}