using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EksiSozluk.WebApp;
using EksiSozluk.WebApp.Infrastructure.Auth;
using EksiSozluk.WebApp.Infrastructure.Services;
using EksiSozluk.WebApp.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("WebApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:8080");
}).AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped(sp =>
{
    return sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebApiClient");
});

builder.Services.AddScoped<AuthTokenHandler>();

builder.Services.AddTransient<IEntryService, EntryService>();
builder.Services.AddTransient<IFavService, FavService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IVoteService, VoteService>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();