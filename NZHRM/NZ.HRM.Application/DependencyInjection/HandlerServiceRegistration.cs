using Microsoft.Extensions.DependencyInjection;
using NZ.HRM.Application.Companies.Handlers;
using NZ.HRM.Application.Locations.Handlers;
using NZ.HRM.Application.Departments.Handlers;
using NZ.HRM.Application.Grades.Handlers;
using NZ.HRM.Application.Sections.Handlers;
using NZ.HRM.Application.EmployeeMasters.Handlers;
using NZ.HRM.Application.EmployeePersonals.Handlers;

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

            // Register Department Handlers
            services.AddScoped<DepartmentCommandHandler>();
            services.AddScoped<DepartmentQueryHandler>();

            // Register Grade Handlers
            services.AddScoped<GradeCommandHandler>();
            services.AddScoped<GradeQueryHandler>();

            // Register Section Handlers
            services.AddScoped<SectionCommandHandler>();
            services.AddScoped<SectionQueryHandler>();

            // Register EmployeeMaster Handlers
            services.AddScoped<EmployeeMasterCommandHandler>();
            services.AddScoped<EmployeeMasterQueryHandler>();

            // Register EmployeePersonal Handlers
            services.AddScoped<EmployeePersonalCommandHandler>();
            services.AddScoped<EmployeePersonalQueryHandler>();

            return services;
        }
    }
}
