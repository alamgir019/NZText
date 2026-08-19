using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NZ.Employee.Infrastructure.Persistence;
using NZ.HRM.Application.EmployeeMasters.Handlers;
using NZ.HRM.Application.Employees.Handlers;
using NZ.HRM.Application.FinancialDetails.Handlers;
using NZ.HRM.Application.MedicalFitnessChecks.Handlers;
using NZ.HRM.Application.PhysicalExaminationSettings.Handlers;
using NZ.HRM.Application.RawPunches.Handlers;
using NZ.HRM.Application.Services;
using NZ.Shared.Contracts.HRM;
using NZ.Employee.Infrastructure.Services;

namespace NZ.Employee.Infrastructure.DependencyInjection;

/// <summary>
/// Employee Service — Employee Profile, Employment History, Document Management
/// </summary>
public static class EmployeeModuleRegistration
{
    public static IServiceCollection AddEmployeeModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<EmployeeDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Command Handlers
        services.AddScoped<EmployeeMasterCommandHandler>();
        services.AddScoped<EmployeeCommandHandler>();
        services.AddScoped<FinancialDetailCommandHandler>();
        services.AddScoped<MedicalFitnessCheckCommandHandler>();
        services.AddScoped<PhysicalExaminationSettingCommandHandler>();

        // Query Handlers
        services.AddScoped<EmployeeMasterQueryHandler>();
        services.AddScoped<GetEnrollmentIdQueryHandler>();
        services.AddScoped<CompleteEmployeeQueryHandler>();
        services.AddScoped<EmployeeQueryHandler>();
        services.AddScoped<FinancialDetailQueryHandler>();
        services.AddScoped<MedicalFitnessCheckQueryHandler>();
        services.AddScoped<PhysicalExaminationSettingQueryHandler>();

        // Contracts implementation
        services.AddScoped<IEmployeeQuery, EmployeeQueryService>();

        return services;
    }
}
