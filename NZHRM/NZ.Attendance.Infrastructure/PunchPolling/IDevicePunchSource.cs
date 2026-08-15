namespace NZ.Attendance.Infrastructure.PunchPolling;

public interface IDevicePunchSource
{
    Task<List<DevicePunchRecord>> PullNewPunchesAsync(string unit, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
}
