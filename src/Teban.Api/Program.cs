using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Teban.Application;
using Teban.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var allowedSpecificOrigins = "allowedSpecificOrigins";

var azureAppConfigConnectionString = builder.Configuration["AppConfigConnectionString"];

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddAzureAppConfiguration(options =>
    {
        options.Connect(azureAppConfigConnectionString)
        .Select(KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName);
    });
});

// Add services to the container.

builder.Services.AddInfrastructureServices(builder);
builder.Services.AddApplicationServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7189", "https://happy-sea-0d649340f.1.azurestaticapps.net", "https://www.teban.app/")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowedSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
