using Microsoft.Extensions.DependencyInjection;
using NZ.HRM.Application.Companies.Handlers;
using NZ.HRM.Application.CompanyLocations.Handlers;
using NZ.HRM.Application.LocationDepartments.Handlers;
using NZ.HRM.Application.SubUnits.Handlers;
using NZ.HRM.Application.Departments.Handlers;
using NZ.HRM.Application.DepartmentSections.Handlers;
using NZ.HRM.Application.Grades.Handlers;
using NZ.HRM.Application.EmployeeNatures.Handlers;
using NZ.HRM.Application.Shifts.Handlers;
using NZ.HRM.Application.Sections.Handlers;
using NZ.HRM.Application.SectionCells.Handlers;
using NZ.HRM.Application.Cells.Handlers;
using NZ.HRM.Application.Designations.Handlers;
using NZ.HRM.Application.EmployeeMasters.Handlers;
using NZ.HRM.Application.EmployeePersonals.Handlers;
using NZ.HRM.Application.Employees.Handlers;
using NZ.HRM.Application.Divisions.Handlers;
using NZ.HRM.Application.Districts.Handlers;
using NZ.HRM.Application.MedicalFitnessChecks.Handlers;
using NZ.HRM.Application.FinancialDetails.Handlers;
using NZ.HRM.Application.PhysicalExaminationSettings.Handlers;
using NZ.HRM.Application.RawPunches.Handlers;
using NZ.HRM.Application.Thanas.Handlers;
using NZ.HRM.Application.Units.Handlers;
using NZ.HRM.Application.RolePermissions.Handlers;
using NZ.HRM.Application.Permissions.Handlers;
using NZ.HRM.Application.UserRoles.Handlers;
using NZ.HRM.Application.UserRoles.Handlers;

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
            services.AddScoped<SubUnitsCommandHandler>();
            services.AddScoped<SubUnitsQueryHandler>();
            // Add this in your service configuration
            // Register Command Handlers
            services.AddScoped<UnitsCommandHandler>();
            // Register Query Handlers
            services.AddScoped<UnitsQueryHandler>();

            // Register CompanyLocation Handlers
            services.AddScoped<CompanyLocationCommandHandler>();
            services.AddScoped<CompanyLocationQueryHandler>();

            // Register LocationDepartment Handlers
            services.AddScoped<LocationDepartmentCommandHandler>();
            services.AddScoped<LocationDepartmentQueryHandler>();

            // Register Department Handlers
            services.AddScoped<DepartmentCommandHandler>();
            services.AddScoped<DepartmentQueryHandler>();
            services.AddScoped<DepartmentSectionCommandHandler>();
            services.AddScoped<DepartmentSectionQueryHandler>();

            // Register Grade Handlers
            services.AddScoped<GradeCommandHandler>();
            services.AddScoped<GradeQueryHandler>();

            // Register EmployeeNature Handlers
            services.AddScoped<EmployeeNatureCommandHandler>();
            services.AddScoped<EmployeeNatureQueryHandler>();

            // Register Shift Handlers
            services.AddScoped<ShiftCommandHandler>();
            services.AddScoped<ShiftQueryHandler>();

            // Register Designation Handlers
            services.AddScoped<DesignationCommandHandler>();
            services.AddScoped<DesignationQueryHandler>();

            // Register Section Handlers
            services.AddScoped<SectionCommandHandler>();
            services.AddScoped<SectionQueryHandler>();
            services.AddScoped<SectionCellCommandHandler>();
            services.AddScoped<SectionCellQueryHandler>();

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
            services.AddScoped<EmployeeCommandHandler>();
            services.AddScoped<CompleteEmployeeQueryHandler>();
            services.AddScoped<EmployeeQueryHandler>();

            // Register Geo Handlers
            services.AddScoped<DivisionQueryHandler>();
            services.AddScoped<DistrictQueryHandler>();
            services.AddScoped<ThanaQueryHandler>();
            // Command handlers for Geo
            services.AddScoped<DivisionCommandHandler>();
            services.AddScoped<DistrictCommandHandler>();
            services.AddScoped<ThanaCommandHandler>();

            // Register Physical Examination Settings Handlers
            services.AddScoped<PhysicalExaminationSettingCommandHandler>();
            services.AddScoped<PhysicalExaminationSettingQueryHandler>();

            // Register Medical Fitness Check Handlers
            services.AddScoped<MedicalFitnessCheckCommandHandler>();
            services.AddScoped<MedicalFitnessCheckQueryHandler>();

            // Register Financial Detail Handlers
            services.AddScoped<FinancialDetailCommandHandler>();
            services.AddScoped<FinancialDetailQueryHandler>();

            // Register RawPunch Handlers
            services.AddScoped<RawPunchCommandHandler>();
            services.AddScoped<Domain.Services.PunchProcessingService>();
            services.AddScoped<Domain.Services.AttendanceProcessingService>();

            // UserRole Handlers
            services.AddScoped<UserRoleCommandHandler>();
            services.AddScoped<UserRoleQueryHandler>();
            // Permission Handlers
            services.AddScoped<PermissionCommandHandler>();
            services.AddScoped<PermissionQueryHandler>();
            // RolePermission Handlers
            services.AddScoped<RolePermissionCommandHandler>();
            services.AddScoped<RolePermissionQueryHandler>();

            return services;
        }
    }
}
