using Microsoft.Extensions.DependencyInjection;

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
            return services;
        }
    }
}
