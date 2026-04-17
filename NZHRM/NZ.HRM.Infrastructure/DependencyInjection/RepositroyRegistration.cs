using Microsoft.Extensions.DependencyInjection;

namespace NZ.HRM.Infrastructure.DependencyInjection
{
    public static class RepositroyRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuPermissionRepository, MenuPermissionRepository>();
            return services;
        }
    }
}
