using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NZ.Attendance.Infrastructure.Persistence;
using NZ.Attendance.Infrastructure.PunchPolling;
using NZ.Attendance.Infrastructure.Repositories;
using NZ.Attendance.Infrastructure.Services;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.Shared.Contracts.Attendance;

namespace NZ.Attendance.Infrastructure.DependencyInjection;

public static class AttendanceModuleRegistration
{
    public static IServiceCollection AddAttendanceModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AttendanceDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IRawPunchRepository, AttRawPunchRepository>();
        services.AddScoped<IProcessedPunchRepository, AttProcessedPunchRepository>();
        services.AddScoped<IAttendanceSummaryQuery, AttendanceSummaryQueryService>();
        // Dashboard query for API
        services.AddScoped< Contracts.IAttendanceDashboardQuery, AttendanceSummaryQueryService>();

        services.Configure<PunchPollingOptions>(configuration.GetSection("PunchPolling"));
        services.AddHttpClient<IDevicePunchSource, VirdiApiDevicePunchSource>();
        services.AddHostedService<PunchPollingBackgroundService>();

        return services;
    }
}
