namespace NZ.HRM.Application.RawPunches.Commands.CreateRawPunch;

public class CreateRawPunchResultDto
{
    public string RawPunchId { get; set; } = string.Empty;
    public string? ProcessedPunchId { get; set; }
    public TimeSpan RawPunchTime { get; set; }
    public TimeSpan AdjustedPunchTime { get; set; }
    public string PunchType { get; set; } = string.Empty;
    public string? ShiftName { get; set; }
}
