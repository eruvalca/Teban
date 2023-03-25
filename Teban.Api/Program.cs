using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Teban.Api.Auth;
using Teban.Api.Mapping;
using Teban.Api.Swagger;
using Teban.Application;
using Teban.Application.Identity;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

string dbConnectionString = config["DefaultConnection"]!;
string symmetricKey = config["SymmetricKeyOptions:SymmetricKey"]!;
string issuer = config["IdentityServiceOptions:Issuer"]!;
string audience = config["IdentityServiceOptions:Audience"]!;

IConfigurationSection symmetricKeyOptions = config.GetSection("SymmetricKeyOptions");
IConfigurationSection identityServiceOptions = config.GetSection("IdentityServiceOptions");

var allowedSpecificOrigins = "allowedSpecificOrigins";

builder.Services.AddDatabaseAndIdentity(dbConnectionString);

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricKey)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin(
                //"https://localhost:7189", "https://happy-sea-0d649340f.1.azurestaticapps.net", "https://www.teban.app"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1.0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = new HeaderApiVersionReader("api-version");
}).AddMvc().AddApiExplorer();

builder.Services.AddControllers();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(x => x.OperationFilter<SwaggerDefaultValues>());

builder.Services.AddApplication(symmetricKeyOptions, identityServiceOptions);

builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        foreach (var description in app.DescribeApiVersions())
        {
            x.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName);
        }
    });
}

app.UseHttpsRedirection();

app.UseCors(allowedSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ValidationMappingMiddleware>();
app.MapControllers();

app.Run();
