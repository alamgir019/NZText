using Microsoft.Extensions.DependencyInjection;
using NZ.HRM.Application.Interface;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Infrastructure.Repositories;

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
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ICellRepository, CellRepository>();
            services.AddScoped<IEmployeeMasterRepository, EmployeeMasterRepository>();
            services.AddScoped<IEmployeePersonalRepository, EmployeePersonalRepository>();
            services.AddScoped<IEmployeeVerificationRepository, EmployeeVerificationRepository>();
            return services;
        }
    }
}
