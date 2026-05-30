using Microsoft.Extensions.Options;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.RawPunches.Commands.CreateRawPunch;
using NZ.HRM.Application.RawPunches.Handlers;

namespace NZ.HRM.WebAPI.Services.PunchPolling;

public class PunchPollingBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IDevicePunchSource _devicePunchSource;
    private readonly IOptions<PunchPollingOptions> _options;
    private readonly ILogger<PunchPollingBackgroundService> _logger;

    public PunchPollingBackgroundService(
        IServiceScopeFactory scopeFactory,
        IDevicePunchSource devicePunchSource,
        IOptions<PunchPollingOptions> options,
        ILogger<PunchPollingBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _devicePunchSource = devicePunchSource;
        _options = options;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var options = _options.Value;

            if (!options.Enabled)
            {
                await Task.Delay(TimeSpan.FromSeconds(Math.Max(5, options.PollIntervalSeconds)), stoppingToken);
                continue;
            }

            try
            {
                var punches = await _devicePunchSource.PullNewPunchesAsync(stoppingToken);

                if (punches.Count > 0)
                {
                    _logger.LogInformation("Pulled {Count} punch records from device source", punches.Count);
                }

                using var scope = _scopeFactory.CreateScope();
                var employeeRepository = scope.ServiceProvider.GetRequiredService<IEmployeeMasterRepository>();
                var rawPunchCommandHandler = scope.ServiceProvider.GetRequiredService<RawPunchCommandHandler>();

                foreach (var punch in punches)
                {
                    var employee = await employeeRepository.GetByEmployeeCodeAsync(punch.EmployeeCode, stoppingToken);
                    if (employee == null)
                    {
                        _logger.LogWarning("Employee not found for employee code {EmployeeCode}", punch.EmployeeCode);
                        continue;
                    }

                    var command = new CreateRawPunchCommand
                    {
                        EmployeeId = employee.Id,
                        EmployeeCode = punch.EmployeeCode,
                        DeviceId = punch.DeviceId ?? options.DeviceId,
                        PunchDate = punch.PunchDateTime.Date,
                        PunchTime = punch.PunchDateTime.TimeOfDay,
                        PunchType = punch.PunchType
                    };

                    await rawPunchCommandHandler.Handle(command, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while polling and processing punches");
            }

            await Task.Delay(TimeSpan.FromSeconds(Math.Max(5, options.PollIntervalSeconds)), stoppingToken);
        }
    }
}
