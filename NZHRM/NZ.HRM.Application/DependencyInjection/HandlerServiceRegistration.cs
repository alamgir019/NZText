using Microsoft.Extensions.DependencyInjection;
using NZ.HRM.Application.Companies.Handlers;
using NZ.HRM.Application.Locations.Handlers;
using NZ.HRM.Application.Departments.Handlers;
using NZ.HRM.Application.Grades.Handlers;
using NZ.HRM.Application.Sections.Handlers;
using NZ.HRM.Application.Cells.Handlers;
using NZ.HRM.Application.EmployeeMasters.Handlers;
using NZ.HRM.Application.EmployeePersonals.Handlers;
using NZ.HRM.Application.Employees.Handlers;
using NZ.HRM.Application.Divisions.Handlers;
using NZ.HRM.Application.Districts.Handlers;
using NZ.HRM.Application.Thanas.Handlers;

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

            // Register Cell Handlers
            services.AddScoped<CellCommandHandler>();
            services.AddScoped<CellQueryHandler>();

            // Register EmployeeMaster Handlers
            services.AddScoped<EmployeeMasterCommandHandler>();
            services.AddScoped<EmployeeMasterQueryHandler>();
            services.AddScoped<GetEnrollmentIdQueryHandler>();

            // Register EmployeePersonal Handlers
            services.AddScoped<EmployeePersonalCommandHandler>();
            services.AddScoped<EmployeePersonalQueryHandler>();

            // Register Complete Employee Handlers
            services.AddScoped<CompleteEmployeeCommandHandler>();
            services.AddScoped<GetCompleteEmployeeQueryHandler>();

            // Register Geo Handlers
            services.AddScoped<DivisionQueryHandler>();
            services.AddScoped<DistrictQueryHandler>();
            services.AddScoped<ThanaQueryHandler>();

            return services;
        }
    }
}
