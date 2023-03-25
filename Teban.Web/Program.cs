using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using Teban.Api.Sdk;
using Teban.UI.Common;
using Teban.UI.Common.Providers;
using Teban.UI.Services;
using Teban.Web;
using Teban.Web.Common;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string tebanApiBaseAddress = builder.HostEnvironment.IsDevelopment()
    ? "https://localhost:7002"
    : "https://localhost:7002";

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ILocalSecureStorage, BrowserLocalSecureStorage>();

builder.Services.AddHttpClient()
    .AddRefitClient<IIdentityApi>()
    .ConfigureHttpClient(x =>
        x.BaseAddress = new Uri(tebanApiBaseAddress));

builder.Services.AddHttpClient()
    .AddRefitClient<IContactsApi>(s => new RefitSettings
    {
        AuthorizationHeaderValueGetter = async () => await s.GetRequiredService<ILocalSecureStorage>().GetAsync("authToken")
    }).ConfigureHttpClient(x =>
        x.BaseAddress = new Uri(tebanApiBaseAddress));

builder.Services.AddHttpClient()
    .AddRefitClient<ICommunicationSchedulesApi>(s => new RefitSettings
    {
        AuthorizationHeaderValueGetter = async () => await s.GetRequiredService<ILocalSecureStorage>().GetAsync("authToken")
    }).ConfigureHttpClient(x =>
        x.BaseAddress = new Uri(tebanApiBaseAddress));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

await builder.Build().RunAsync();
