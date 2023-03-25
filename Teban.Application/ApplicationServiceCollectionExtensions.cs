using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Teban.Application.Identity;
using Teban.Application.Models;
using Teban.Application.Options;
using Teban.Application.Persistence;
using Teban.Application.Persistence.Interceptors;
using Teban.Application.Services;

namespace Teban.Application;
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        IConfigurationSection symmetricKeyOptions, IConfigurationSection identityServiceOptions)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.Configure<SymmetricKeyOptions>(symmetricKeyOptions);
        services.AddSingleton<ISymmetricKeyService, SymmetricKeyService>();

        services.Configure<IdentityServiceOptions>(identityServiceOptions);
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<ICommunicationScheduleService, CommunicationScheduleService>();

        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        return services;
    }

    public static IServiceCollection AddDatabaseAndIdentity(this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(connectionString));

        services.AddIdentity<TebanUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedAccount = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}
