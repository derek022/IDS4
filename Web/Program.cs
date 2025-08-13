using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc",options =>
    {
        options.Authority = UrlOptions.IdentityUrl;
        options.ClientId = "sample_mvc_client";
        options.ClientSecret = "sample_client_secret";
        options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.UsePkce = false;
        options.SaveTokens = true;
        options.Scope.Add("sample_api");
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();