using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NZ.Leave.Application.Interfaces.Repositories;
using NZ.Leave.Application.LeaveRequests.Commands.CreateLeaveRequests;
using NZ.Leave.Application.LeaveRequests.Commands.DeleteLeaveRequest;
using NZ.Leave.Application.LeaveRequests.Commands.UpdateLeaveRequest;
using NZ.Leave.Application.LeaveRequests.Queries.GetLeaveRequests;
using NZ.Leave.Application.LeaveTypes.Handlers;
using NZ.Leave.Infrastructure.Persistence;
using NZ.Leave.Infrastructure.Repositories;
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

        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<GetAllLeaveTypesQueryHandler>();

        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<CreateLeaveRequestsCommandHandler>();
        services.AddScoped<UpdateLeaveRequestCommandHandler>();
        services.AddScoped<DeleteLeaveRequestCommandHandler>();
        services.AddScoped<GetLeaveRequestsQueryHandler>();

        return services;
    }
}
