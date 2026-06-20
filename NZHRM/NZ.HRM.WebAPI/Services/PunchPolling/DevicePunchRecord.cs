namespace NZ.HRM.WebAPI.Services.PunchPolling;

public class DevicePunchRecord
{
    public string EmployeeCode { get; set; } = string.Empty;
    public DateOnly PunchDate { get; set; }
    public TimeOnly PunchTime { get; set; }
    public string? PunchType { get; set; }
    public string? DeviceId { get; set; }
    public string? DeviceLocation { get; set; }
    public string? VerificationMode { get; set; }
    public string? PunchSource { get; set; }
}
