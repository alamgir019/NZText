namespace NZ.HRM.WebAPI.Services.PunchPolling;

public class DevicePunchRecord
{
    public string EmployeeCode { get; set; } = string.Empty;
    public DateTime PunchDateTime { get; set; }
    public string? PunchType { get; set; }
    public string? DeviceId { get; set; }
}
