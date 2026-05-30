using Microsoft.Extensions.Options;

namespace NZ.HRM.WebAPI.Services.PunchPolling;

public class SimulatedDevicePunchSource : IDevicePunchSource
{
    private readonly PunchPollingOptions _options;

    public SimulatedDevicePunchSource(IOptions<PunchPollingOptions> options)
    {
        _options = options.Value;
    }

    public Task<List<DevicePunchRecord>> PullNewPunchesAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<DevicePunchRecord>());
    }
}
