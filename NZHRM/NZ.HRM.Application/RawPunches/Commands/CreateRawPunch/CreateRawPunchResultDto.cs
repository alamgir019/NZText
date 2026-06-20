namespace NZ.HRM.Application.RawPunches.Commands.CreateRawPunch;

public class CreateRawPunchResultDto
{
    public string RawPunchId { get; set; } = string.Empty;
    public string? ProcessedPunchId { get; set; }
    public TimeOnly RawPunchTime { get; set; }
    public TimeOnly AdjustedPunchTime { get; set; }
    public string PunchType { get; set; } = string.Empty;
    public string? ShiftName { get; set; }
}
