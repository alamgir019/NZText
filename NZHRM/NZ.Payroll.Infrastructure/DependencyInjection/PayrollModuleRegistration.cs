using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NZ.Payroll.Application.Interfaces;
using NZ.Payroll.Application.Services;
using NZ.Payroll.Infrastructure.Persistence;
using NZ.Payroll.Infrastructure.Services;
using NZ.Shared.Contracts.HRM;

namespace NZ.Payroll.Infrastructure.DependencyInjection;

public static class PayrollModuleRegistration
{
    public static IServiceCollection AddPayrollModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PayrollDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        // Application services
        services.AddScoped<IPayrollCalculationService, PayrollCalculationService>();
        services.AddScoped<IPayrollProcessingService, PayrollProcessingService>();

        // HRM cross-module contract implementation (reads from shared DB)
        services.AddScoped<IEmployeeQuery, EmployeeQueryService>();

        return services;
    }
}
