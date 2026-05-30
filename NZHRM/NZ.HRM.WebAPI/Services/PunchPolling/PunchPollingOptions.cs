namespace NZ.HRM.WebAPI.Services.PunchPolling;

public class PunchPollingOptions
{
    public bool Enabled { get; set; } = false;
    public int PollIntervalSeconds { get; set; } = 60;
    public string DeviceId { get; set; } = "SIM-DEVICE-01";
}
