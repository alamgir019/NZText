using Microsoft.Extensions.DependencyInjection;
using NZ.HRM.Application.Companies.Handlers;
using NZ.HRM.Application.Locations.Handlers;

namespace NZ.HRM.Application.DependencyInjection
{
    public static class HandlerServiceRegistration
    {
        public static IServiceCollection AddHandlerServices(this IServiceCollection services)
        {
            services.AddScoped<UserCommandHandler>();
            services.AddScoped<UserQueryHandler>();
            services.AddScoped<RoleCommandHandler>();
            services.AddScoped<RoleQueryHandler>();
            services.AddScoped<MenuCommandHandler>();
            services.AddScoped<MenuQueryHandler>();
            services.AddScoped<MenuPermissionCommandHandler>();
            services.AddScoped<MenuPermissionQueryHandler>();
            services.AddScoped<LocationCommandHandler>();
            services.AddScoped<LocationQueryHandler>();
            // Add this in your service configuration
            // Register Command Handlers
            services.AddScoped<CompaniesCommandHandler>();
            // Register Query Handlers
            services.AddScoped<CompaniesQueryHandler>();
            return services;
        }
    }
}
