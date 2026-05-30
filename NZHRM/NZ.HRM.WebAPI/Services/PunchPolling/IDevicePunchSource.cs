namespace NZ.HRM.WebAPI.Services.PunchPolling;

public interface IDevicePunchSource
{
    Task<List<DevicePunchRecord>> PullNewPunchesAsync(CancellationToken cancellationToken);
}
