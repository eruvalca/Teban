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

builder.Services.AddScoped<ApiHeadersHandler>();

builder.Services
    .AddRefitClient<IIdentityApi>()
    .ConfigureHttpClient(x =>
        x.BaseAddress = new Uri(tebanApiBaseAddress));

builder.Services
    .AddRefitClient<IContactsApi>()
    .ConfigureHttpClient(x =>
        x.BaseAddress = new Uri(tebanApiBaseAddress))
    .AddHttpMessageHandler<ApiHeadersHandler>();

builder.Services
    .AddRefitClient<ICommunicationSchedulesApi>()
    .ConfigureHttpClient(x =>
        x.BaseAddress = new Uri(tebanApiBaseAddress))
    .AddHttpMessageHandler<ApiHeadersHandler>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IContactsApiService, ContactsApiService>();

await builder.Build().RunAsync();