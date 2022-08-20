using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Portal.Blazor;
using Portal.Blazor.Authentication;
using Portal.Blazor.Authentication.Interfaces;
using Portal.Blazor.Authentication.Services;
using Portal.Blazor.Interfaces;
using Portal.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IUserEndpoints, UsersEndpoint>();
builder.Services.AddScoped<IProductsEndpoint, ProductsEndpoint>();

builder.Services.AddScoped(sp => new HttpClient
{ BaseAddress = new Uri(builder.Configuration.GetValue<string>("baseUrl")) });

await builder.Build().RunAsync();