// See https://aka.ms/new-console-template for more information

using IdentityModel;
using IdentityModel.Client;
using Shared;

Console.WriteLine("Hello, World!");

var client = new HttpClient();
var disco =await client.GetDiscoveryDocumentAsync(UrlOptions.IdentityUrl);

if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
{
    Address = disco.TokenEndpoint,
    ClientId = "sample_pass_client",
    ClientSecret = "sample_client_secret",
    GrantType = OidcConstants.GrantTypes.Password,
    UserName = "admin",
    Password = "password"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine(tokenResponse.Json);



var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync(UrlOptions.DefaultApiUrl + "api/identity");

Console.WriteLine(await response.Content.ReadAsStringAsync());

