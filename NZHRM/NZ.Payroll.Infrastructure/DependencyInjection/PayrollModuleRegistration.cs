using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PayIncrementHistories.Handlers;
using NZ.Payroll.Infrastructure.Persistence;
using NZ.Payroll.Infrastructure.Repositories;

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

        // Pay increment history
        services.AddScoped<IPayIncrementHistoryRepository, PayIncrementHistoryRepository>();
        services.AddScoped<CreatePayIncrementHistoryHandler>();

        return services;
    }
}
