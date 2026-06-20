namespace NZ.HRM.WebAPI.Services.PunchPolling;

public interface IDevicePunchSource
{
    Task<List<DevicePunchRecord>> PullNewPunchesAsync(string unit, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
}
