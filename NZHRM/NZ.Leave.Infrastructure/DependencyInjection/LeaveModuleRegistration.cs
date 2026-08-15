using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NZ.Leave.Infrastructure.Persistence;
using NZ.Leave.Infrastructure.Services;
using NZ.Shared.Contracts.Leave;

namespace NZ.Leave.Infrastructure.DependencyInjection;

public static class LeaveModuleRegistration
{
    public static IServiceCollection AddLeaveModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<LeaveDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ILeaveBalanceQuery, LeaveBalanceQueryService>();

        return services;
    }
}
