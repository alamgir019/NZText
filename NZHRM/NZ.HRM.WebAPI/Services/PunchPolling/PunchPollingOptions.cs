namespace NZ.HRM.WebAPI.Services.PunchPolling;

public class PunchPollingOptions
{
    public bool Enabled { get; set; } = false;
    public int PollIntervalSeconds { get; set; } = 300;
    public string DeviceName { get; set; } = "VIRDI-01";
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public List<string> Units { get; set; } = new();
    public int LookbackDays { get; set; } = 1;
    public string DeviceLocation { get; set; } = string.Empty;
    public string VerificationMode { get; set; } = "API";
    public string PunchSource { get; set; } = "VirdiApi";
}
