using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Teban.UI.Common.Providers;
using Teban.UI.Interfaces;
using Teban.UI.Services;
using Teban.Web;
using Teban.Web.Common;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

if (builder.HostEnvironment.IsDevelopment())
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7291/api/v1/") });
}
else
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://tebanapi.azurewebsites.net/api/v1/") });
}

builder.Services.AddScoped<IdentityClientService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ILocalSecureStorage, BrowserLocalSecureStorage>();
builder.Services.AddScoped<BudgetsService>();
builder.Services.AddScoped<AccountsService>();
builder.Services.AddScoped<CategoryGroupsService>();
builder.Services.AddScoped<CategoriesService>();
builder.Services.AddScoped<AccountTransactionsService>();
builder.Services.AddScoped<TransactionEntriesService>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();

await builder.Build().RunAsync();
