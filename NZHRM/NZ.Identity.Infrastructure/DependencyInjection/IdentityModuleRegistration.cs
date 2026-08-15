using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NZ.HRM.Application.DependencyInjection;
// Handler registrations are provided by the application project registration helper
using NZ.Identity.Infrastructure.Persistence;

namespace NZ.Identity.Infrastructure.DependencyInjection;

/// <summary>
/// Identity Service — Authentication, Authorization, Role Management, MFA, SSO
/// </summary>
public static class IdentityModuleRegistration
{
    public static IServiceCollection AddIdentityModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        // Register handlers from the main application module so we don't duplicate
        // handler implementations or rely on non-existent namespaces inside this
        // infrastructure project. The application project exposes AddHandlerServices()
        // which registers all command/query handlers used across the host.
        services.AddHandlerServices();

        return services;
    }
}
